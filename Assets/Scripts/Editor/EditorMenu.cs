using Enums;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class EditorMenu : EditorWindow
    {
        private static EditorSettings settings;

        [MenuItem("Board Adjuster/Gameplay Editor Settings")]
        public static void ShowWindow()
        {
            GetWindow<EditorMenu>("Editor Settings");
            LoadSettings();
        }

        private void OnGUI()
        {
            if (settings == null)
            {
                LoadSettings();
            }

            EditorGUILayout.LabelField("Admin Settings", EditorStyles.boldLabel);
            settings.adminCreateItemTouch = EditorGUILayout.Toggle("Admin Create Item Touch", settings.adminCreateItemTouch);
            settings.itemType = (ItemTypeEnum) EditorGUILayout.EnumPopup("Item Type", settings.itemType);
            settings.matchType = (MatchTypeEnum) EditorGUILayout.EnumPopup("Match Type", settings.matchType);

            if (GUILayout.Button("Save Settings"))
            {
                SaveSettings();
            }
        }

        private static void LoadSettings()
        {
            settings = AssetDatabase.LoadAssetAtPath<EditorSettings>("Assets/Scripts/ScriptableObjects/EditorSettings/EditorSettings.asset");
        }

        private static void SaveSettings()
        {
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
        }
    }
}