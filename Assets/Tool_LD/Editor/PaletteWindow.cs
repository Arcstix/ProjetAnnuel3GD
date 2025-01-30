using System.Collections.Generic;
using Tool_LD.Script;
using UnityEditor;
using UnityEngine;

namespace Tool_LD.Editor
{
    public class PaletteWindow : EditorWindow
    {
        private enum Rotation
        {
            X,
            Y,
            Z
        }
        
        private SelectionWindow selectionWindow;
        private int selectedIndex; // Index of selected item
        
        private Vector2 scrollPosition; // For scrolling
        private const int CellSize = 100; // Cell size (width and height)
        private const int Padding = 5;  // Spacing between cells

        private Palette[] palettes;
        private List<string> optionsList;
        private string paletteName = "";
        private string selectedPrefab;

        private GameObject previewObject;
        private bool brushPreviewActive;
        private bool brushSuppressActive;
        
        private Rotation currentRotation = Rotation.X;
        private float rotationSpeed = 15f;
        private Quaternion previewRotation = Quaternion.identity;
        Vector3 axis = Vector3.right;
        Color currentColor = Color.white;
        
        [MenuItem("Tool/PaletteWindow")]
        private static void ShowWindow()
        {
            var window = GetWindow<PaletteWindow>();
            window.titleContent = new GUIContent("Palette Window");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            
            GUILayout.BeginHorizontal();
            CreateDropDown();
            GUILayout.Space(10);
            SuppressPaletteButton();
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);

            CreateButton();
            GUILayout.Space(10);
            
            // Separator
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            
            // Displays the list in a scrollable area
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            if (palettes.Length > 0)
            {
                DisplayPrefabs(palettes[selectedIndex]);
            }
            
            GUILayout.EndScrollView();

            if (brushPreviewActive)
            {
                Event e = Event.current;
                UpdateCurrentRotationAxe(e);
            }
        }

        private void SuppressPaletteButton()
        {
            Color defaultColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Suppress Palette"))
            {
                // Show a Display Dialogue to make sure the user want to suppress the active palette
                bool confirmed = EditorUtility.DisplayDialog("Suppress Active Palette", "Are you sure you want to suppress the active palette ?", "Yes", "No");

                if (confirmed)
                {
                    SuppressActivePalette();
                }
            }
            GUI.backgroundColor = defaultColor;
        }

        private void SuppressActivePalette()
        {
            string path = AssetDatabase.GetAssetPath(palettes[selectedIndex]);
            AssetDatabase.DeleteAsset(path);
            selectedIndex = Mathf.Max(0, selectedIndex - 1);
            LoadAllPalettes();
        }
        
        private void CreateDropDown()
        {
            // Show the dropdown
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (UpdateOptions().ToArray().Length <= 0)
            {
                EditorGUILayout.LabelField("No Palette present in the project");
            }
            else
            {
                selectedIndex = EditorGUILayout.Popup(selectedIndex, UpdateOptions().ToArray());
                if (selectionWindow != null)
                {
                    selectionWindow.UpdateSelectedPalette(palettes[selectedIndex]);
                }
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void CreateButton()
        {
            GUILayout.BeginHorizontal();
            
            paletteName = EditorGUILayout.TextField(paletteName);
            
            if (GUILayout.Button("New Palette") && !string.IsNullOrWhiteSpace(paletteName))
            {
                // Create new Palette Scriptable Object 
                CreateNewPalette();
                
                // Show the popup with updated options (palette)
                selectedIndex = EditorGUILayout.Popup(ArrayUtility.IndexOf(UpdateOptions().ToArray(), paletteName), UpdateOptions().ToArray());
                
                // Open the prefab selection window if not already open
                CreateNewWindow();
            }

            if (GUILayout.Button("Edit Palette"))
            {
                // Open the prefab selection window if not already open
                CreateNewWindow();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            if (brushPreviewActive)
            {
                if (GUILayout.Button("Disable Preview Brush"))
                {
                    // Quit Preview Mode
                    brushPreviewActive = false;
                    DestroyImmediate(previewObject);
                }
            }
            else
            {
                if (GUILayout.Button("Enable Preview Brush"))
                {
                    // Enter Preview Mode
                    brushPreviewActive = true;
                    brushSuppressActive = false;
                }
            }
            
            GUILayout.Space(10);
            
            if (brushSuppressActive)
            {
                if (GUILayout.Button("Disable Suppress Brush"))
                {
                    // Quit Suppress Mode
                    brushSuppressActive = false;
                }
            }
            else
            {
                if (GUILayout.Button("Enable Suppress Brush"))
                {
                    // Enter Suppress Mode
                    brushSuppressActive = true;
                    if (brushPreviewActive)
                    {
                        brushPreviewActive = false;
                        if (previewObject != null)
                        {
                            DestroyImmediate(previewObject);
                        }
                    }
                    
                }
            }
        }

        private void CreateNewWindow()
        {
            selectionWindow = GetWindow<SelectionWindow>();
            selectionWindow.titleContent = new GUIContent("Selection Window");
            selectionWindow.Show();
        }

        private void LoadAllPalettes()
        {
            LoadAllAssetsOfType(out palettes);
        }
        
        private void LoadAllAssetsOfType<T>(out T[] assets) where T : Palette
        {
            string[] guids = AssetDatabase.FindAssets("t:"+typeof(T));
            assets = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
        }
        
        private List<string> UpdateOptions()
        {
            // Load all palettes in case the user destroy a palette
            LoadAllPalettes();
            optionsList = new List<string>();
            
            // Add all SO palette name to the option list for the dropdown
            foreach (var palette in palettes)
            {
                optionsList.Add(palette.name);
            }

            return optionsList;
        }

        private void CreateNewPalette()
        {
            // Créer un nouvel objet Palette
            Palette newPaletteData = ScriptableObject.CreateInstance<Palette>();
            
            // Enregistrer le nouvel objet dans le projet
            AssetDatabase.CreateAsset(newPaletteData, "Assets/Tool_LD/SO Palette/" + paletteName + ".asset");
            AssetDatabase.SaveAssets();

            // Recharger les Palettes pour afficher le nouveau
            LoadAllPalettes();
        }
        
        private void DisplayPrefabs(Palette palette)
        {
            // Determines the number of columns according to the width of the window
            int columns = Mathf.Max(1, (int)(position.width / (CellSize + Padding)));
            
            int rowCount = Mathf.CeilToInt((float)palette.listOfPrefabPath.Count / columns);

            for (int row = 0; row < rowCount; row++)
            {
                GUILayout.BeginHorizontal();

                for (int col = 0; col < columns; col++)
                {
                    int index = row * columns + col;
                    if (index >= palette.listOfPrefabPath.Count)
                        break;
                        
                    string path = palette.listOfPrefabPath[index];
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    
                    GUILayout.BeginVertical(GUILayout.Width(CellSize), GUILayout.Height(CellSize));
                        
                    // Displays the prefab preview (AssetPreview)
                    Texture2D previewTexture = AssetPreview.GetAssetPreview(prefab);
                    if (previewTexture != null)
                    {
                        if (GUILayout.Button(previewTexture, GUILayout.Width(64), GUILayout.Height(64)))
                        {
                            if (selectionWindow != null)
                            {
                                // Remove from the SO list.
                                EditorUtility.SetDirty(palette);
                                palette.listOfPrefabPath.Remove(path);
                                AssetDatabase.SaveAssets();
                                AssetDatabase.Refresh();
                            }
                            else
                            {
                                selectedPrefab = path;
                                DestroyImmediate(previewObject);

                                if (!brushPreviewActive && !brushSuppressActive)
                                {
                                    ReplaceObject(prefab);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Pas d'aperçu", GUILayout.Width(64), GUILayout.Height(64)))
                        {
                            if (selectionWindow != null)
                            {
                                EditorUtility.SetDirty(palette);
                                palette.listOfPrefabPath.Remove(path);
                                AssetDatabase.SaveAssets();
                                AssetDatabase.Refresh();
                            }
                            else
                            {
                                selectedPrefab = path;
                                DestroyImmediate(previewObject);
                                
                                if (!brushPreviewActive && !brushSuppressActive)
                                {
                                    ReplaceObject(prefab);
                                }
                            }
                        }
                    }
                    GUILayout.Label(prefab.name, GUILayout.ExpandWidth(true));
                    
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();
            }
        }

        private void ReplaceObject(GameObject prefab)
        {
            if (Selection.activeGameObject == null) return;
            
            GameObject activeObject = Selection.activeGameObject;
            if (activeObject.activeInHierarchy)
            {
                // Give the new Prefab the position and rotation of the old one to replace
                GameObject newPrefab = Instantiate(prefab, activeObject.transform.position, activeObject.transform.rotation);
                Undo.RegisterCreatedObjectUndo(newPrefab, "New Object");
                // Delete the one to replace 
                Undo.DestroyObjectImmediate(activeObject);
            }
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            // Si un prefab est sélectionné et que l'on est en Preview Mode
            if (!string.IsNullOrWhiteSpace(selectedPrefab) && brushPreviewActive && !brushSuppressActive)
            {
                PreviewMode();
            }

            if (brushSuppressActive && !brushPreviewActive)
            {
                SuppressMode();
            }
        }

        private void SuppressMode()
        {
            Event e = Event.current;
            
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                
            //Shoot Raycast for detecting object in the scene
            if (Physics.Raycast(ray, out RaycastHit objectHit, Mathf.Infinity, ~LayerMask.GetMask("Ignore Raycast"), QueryTriggerInteraction.Collide))
            {
                // TODO : Make visible what we hit
                ShowObjectHit(objectHit.collider.gameObject);
                
                // Check if user click in the scene
                if (e.type == EventType.MouseDown && e.button == 0)
                {
                    // Destroy GameObject hit by the Raycast
                    Undo.DestroyObjectImmediate(objectHit.collider.gameObject.transform.parent != null
                        ? objectHit.collider.gameObject.transform.parent.gameObject
                        : objectHit.collider.gameObject);
                    e.Use();
                }
            }
        }

        private void ShowObjectHit(GameObject objectHit)
        {
            GameObject[] objectsHit;
            // Check if the object hit is a child of an object
            if (objectHit.transform.parent != null)
            {
                objectsHit = new GameObject[objectHit.transform.parent.childCount];
                for (int i = 0; i < objectsHit.Length; i++)
                {
                    objectsHit[i] = objectHit.transform.parent.GetChild(i).gameObject;
                }
            }
            else if(objectHit.transform.childCount > 0)
            {
                // In this case the raycast hit the parent and he has child object
                objectsHit = new GameObject[objectHit.transform.childCount];
                for (int i = 0; i < objectsHit.Length; i++)
                {
                    objectsHit[i] = objectHit.transform.GetChild(i).gameObject;
                }
            }
            else
            {
                // In this case the Raycast hit an object without parent and child
                objectsHit = new GameObject[1];
                objectsHit[0] = objectHit.gameObject;
            }
            
            Color color = Color.red;
            Handles.DrawOutline(objectsHit, color, color, 0.5f);
        }

        private void PreviewMode()
        {
            Event e = Event.current;
                
            RotationPreview(e);
                
            UpdateCurrentRotationAxe(e);
                
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                
            //Shoot Raycast for the position of the preview object and set a Layer to ignore himself
            if (Physics.Raycast(ray, out RaycastHit previewHit, 10f, ~LayerMask.GetMask("Ignore Raycast"), QueryTriggerInteraction.Collide))
            {
                // In case we don't have preview Object showing in scene 
                if (previewObject == null || previewObject.name !=
                    AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab).name + "(Clone)")
                {
                    previewObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab), previewHit.point, previewRotation);
                    previewObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // Ignorer par le Raycast
                    foreach (Transform child in previewObject.transform)
                    {
                        child.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    }
                }
                    
                previewObject.transform.position = previewHit.point;
            }
            else
            {
                // In case the Raycast hit nothing 
                Vector3 spawnPosition = ray.origin + ray.direction * 10f;
                    
                if (previewObject == null || previewObject.name !=
                    AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab).name + "(Clone)")
                {
                    previewObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab), spawnPosition, previewRotation);
                    previewObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // Ignorer par le Raycast
                    foreach (Transform child in previewObject.transform)
                    {
                        child.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    }
                }
                else
                {
                    previewObject.transform.position = spawnPosition;
                }
            }

            // Check if user click in the scene
            if (e.type == EventType.MouseDown && e.button == 0)
            {
                // DestroyPreview Object and replace by the good prefab (with good Layer...)
                Vector3 previewPosition = previewObject.transform.position;
                previewRotation = previewObject.transform.rotation;
                DestroyImmediate(previewObject);
                GameObject newPrefab = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab), previewPosition, previewRotation);
                Undo.RegisterCreatedObjectUndo(newPrefab, "new Prefab");
                e.Use();
            }
        }

        private void RotationPreview(Event e)
        {
            // Ensure previewObject exists before doing anything
            if (previewObject == null)
                return; 
            
            if (e.type == EventType.ScrollWheel && e.button != 1)
            {
                // Calculate the rotation
                float delta = e.delta.y * rotationSpeed;
                
                switch (currentRotation)
                {
                    case Rotation.X:
                        previewObject.transform.Rotate(Vector3.right, -delta, Space.World);
                        axis = Vector3.right;
                        currentColor = Color.red;
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                    case Rotation.Y:
                        previewObject.transform.Rotate(Vector3.up, -delta, Space.World);
                        axis = Vector3.up;
                        currentColor = Color.green;
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                    case Rotation.Z:
                        previewObject.transform.Rotate(Vector3.forward, -delta, Space.World);
                        axis = Vector3.forward;
                        currentColor = Color.blue;
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                }
                e.Use();
            }
            DrawRotationHandle();
        }

        private void DrawRotationHandle()
        {
            // Calculate dynamic radius for the handle
            float radius = HandleUtility.GetHandleSize(previewObject.transform.position) * 1f;
            Handles.color = currentColor;    
            // Draw the handle
            Handles.DrawWireDisc(previewObject.transform.position, axis, radius, 10f);
            SceneView.RepaintAll();
        }

        private void UpdateCurrentRotationAxe(Event e)
        {
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.L)
            {
                currentRotation = Rotation.X;
                axis = Vector3.right;
                currentColor = Color.red;
                e.Use();
            }
                
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.P)
            {
                currentRotation = Rotation.Y;
                axis = Vector3.up;
                currentColor = Color.green;
                e.Use();
            }
                
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.M)
            {
                currentRotation = Rotation.Z;
                axis = Vector3.forward;
                currentColor = Color.blue;
                e.Use();
            }
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
            LoadAllPalettes();
        }
        private void OnDisable()
        {
            if (previewObject != null)
            {
                DestroyImmediate(previewObject);
            }
            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }
}