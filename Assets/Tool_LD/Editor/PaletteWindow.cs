using UnityEditor;
using UnityEngine;

namespace Tool_LD.Editor
{
    public class PaletteWindow : EditorWindow
    {
        private SelectionWindow selectionWindow;
        private int selectedIndex = 0; // Index of selected item
        private readonly string[] options = new string[] { "Option 1", "Option 2", "Option 3" };
        
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
        }

        private void CreateDropDown()
        {
            // Show the dropdown
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            selectedIndex = EditorGUILayout.Popup(selectedIndex, options);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void CreateButton()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("New Palette"))
            {
                // Create new Palette Scriptable Object 
                // Open the prefab selection window if not already open
                CreateNewWindow();
            }

            if (GUILayout.Button("Save Palette"))
            {
                // Update the list in the selected palette SO
            }

            if (GUILayout.Button("Edit Palette"))
            {
                // Open the prefab selection window if not already open
                CreateNewWindow();
            }
            GUILayout.EndHorizontal();
        }

        private void CreateNewWindow()
        {
            selectionWindow = GetWindow<SelectionWindow>();
            selectionWindow.titleContent = new GUIContent("Selection Window");
            selectionWindow.Show();
        }
    }
}