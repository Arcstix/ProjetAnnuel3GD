using System;
using System.Collections.Generic;
using Tool_LD.Script;
using UnityEditor;
using UnityEngine;

namespace Tool_LD.Editor
{
    public class PaletteWindow : EditorWindow
    {
        public enum Rotation
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
        private bool brushActive;
        
        private Rotation currentRotation = Rotation.X;
        private float rotationSpeed = 1f;
        
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
            CreateDropDown();
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
        }

        private void OnEnable()
        {
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

            if (brushActive)
            {
                if (GUILayout.Button("Disable Brush"))
                {
                    brushActive = false;
                    DestroyImmediate(previewObject);
                }
            }
            else
            {
                if (GUILayout.Button("Enable Brush"))
                {
                    brushActive = true;
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
                                SceneView.duringSceneGui -= OnSceneGUI; // Retirer les anciens écouteurs
                                DestroyImmediate(previewObject);
                                SceneView.duringSceneGui += OnSceneGUI;
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
                                SceneView.duringSceneGui -= OnSceneGUI; // Retirer les anciens écouteurs
                                DestroyImmediate(previewObject);
                                SceneView.duringSceneGui += OnSceneGUI;
                            }
                        }
                    }
                    
                    GUILayout.Label(prefab.name, GUILayout.ExpandWidth(true));
                    
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();
            }
        }
        
        private void OnSceneGUI(SceneView sceneView)
        {
            // Si un prefab est sélectionné
            if (!string.IsNullOrWhiteSpace(selectedPrefab) && brushActive)
            {
                Event e = Event.current;
                
                UpdateCurrentRotationAxe(e);
                
                Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit previewHit, Mathf.Infinity, ~LayerMask.GetMask("Ignore Raycast"), QueryTriggerInteraction.Ignore))
                {
                    if (previewObject == null || previewObject.name !=
                        AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab).name + "(Clone)")
                    {
                        previewObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab), previewHit.point, Quaternion.identity);
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
                    Vector3 spawnPosition = ray.origin + ray.direction * 20f;
                    if (previewObject != null)
                    {
                        previewObject.transform.position = spawnPosition;
                    }
                    else
                    {
                        previewObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab), spawnPosition, Quaternion.identity);
                        previewObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // Ignorer par le Raycast
                        foreach (Transform child in previewObject.transform)
                        {
                            child.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                        }
                    }
                }

                RotationPreview(e);

                // Vérifier si l'utilisateur clique dans la scène
                if (e.type == EventType.MouseDown && e.button == 0)
                {
                    // DestroyPreview Object and replace by the good prefab
                    Vector3 previewPosition = previewObject.transform.position;
                    Quaternion previewRotation = previewObject.transform.rotation;
                    DestroyImmediate(previewObject);
                    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~LayerMask.GetMask("Ignore Raycast")))
                    {
                        if (hit.collider.gameObject.transform.parent != null && hit.collider.gameObject.transform.parent.name == AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab).name + "(Clone)")
                        {
                            DestroyImmediate(hit.collider.gameObject.transform.parent.gameObject);
                        }
                        else
                        {
                            // Instancier le prefab sur la surface cliquée
                            Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab), previewPosition, previewRotation);
                        }
                    }
                    else
                    {
                        // Instancier le prefab sur la surface cliquée
                        Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(selectedPrefab), previewPosition, previewRotation);
                    }

                    // Marquer l'événement comme utilisé
                    e.Use();
                }
            }
        }

        private void RotationPreview(Event e)
        {
            if (e.type == EventType.ScrollWheel)
            {
                // Calcule la rotation en fonction de la direction de la molette
                float delta = e.delta.y * rotationSpeed;
                switch (currentRotation)
                {
                    case Rotation.X:
                        previewObject.transform.Rotate(Vector3.right, -delta, Space.World);
                        break;
                    case Rotation.Y:
                        previewObject.transform.Rotate(Vector3.up, -delta, Space.World);
                        break;
                    case Rotation.Z:
                        previewObject.transform.Rotate(Vector3.forward, -delta, Space.World);
                        break;
                }
                e.Use();
            }
        }

        private void UpdateCurrentRotationAxe(Event e)
        {
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.X)
            {
                currentRotation = Rotation.X;
                e.Use();
            }
                
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Y)
            {
                currentRotation = Rotation.Y;
                Debug.Log(currentRotation);
                e.Use();
            }
                
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Z)
            {
                Debug.Log(currentRotation);
                currentRotation = Rotation.Z;
                e.Use();
            }
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }
}