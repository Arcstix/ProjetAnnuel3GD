using System.Collections.Generic;
using Tool_LD.Script;
using UnityEditor;
using UnityEngine;

namespace Tool_LD.Editor
{
    public class SelectionWindow : EditorWindow
    {
        // List to store prefab paths
        private List<string> prefabPaths = new List<string>();
        
        private Vector2 scrollPosition; // For scrolling
        private const int CellSize = 100; // Cell size (width and height)
        private const int Padding = 5;  // Spacing between cells

        private Palette activePalette;
        
        private void OnGUI()
        {
            ShowAllPrefabInProject();
        }
        
        private void FindAllPrefabs()
        {
            prefabPaths.Clear();
            string[] guids = AssetDatabase.FindAssets("t:Prefab");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                prefabPaths.Add(path);
            }
        }
        
        private void ShowAllPrefabInProject()
        {
            FindAllPrefabs();
            
            // Displays the list in a scrollable area
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            
            DisplayPrefabs();
            
            GUILayout.EndScrollView();
        }

        private void DisplayPrefabs()
        {
            // Determines the number of columns according to the width of the window
            int columns = Mathf.Max(1, (int)(position.width / (CellSize + Padding)));
            
            int rowCount = Mathf.CeilToInt((float)prefabPaths.Count / columns);

            for (int row = 0; row < rowCount; row++)
            {
                GUILayout.BeginHorizontal();

                for (int col = 0; col < columns; col++)
                {
                    int index = row * columns + col;
                    if (index >= prefabPaths.Count)
                        break;
                        
                    string path = prefabPaths[index];
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                        
                    GUILayout.BeginVertical(GUILayout.Width(CellSize), GUILayout.Height(CellSize));
                        
                    // Displays the prefab preview (AssetPreview)
                    Texture2D previewTexture = AssetPreview.GetAssetPreview(prefab);
                    if (previewTexture != null)
                    {
                        if (GUILayout.Button(previewTexture, GUILayout.Width(64), GUILayout.Height(64)))
                        {
                            // Ajoute à la liste dans le scriptable object.
                            activePalette.listOfPrefabPath.Add(path);
                            Selection.activeGameObject = prefab;
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Pas d'aperçu", GUILayout.Width(64), GUILayout.Height(64)))
                        {
                            // Ajoute à la liste dans le scriptable object.
                            activePalette.listOfPrefabPath.Add(path);
                            Selection.activeGameObject = prefab;
                        }
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();
            }
        }

        public void UpdateSelectedPalette(Palette palette)
        {
            activePalette = palette;
        }
    }
}