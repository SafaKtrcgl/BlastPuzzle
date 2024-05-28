#if UNITY_EDITOR

using Enums;
using UnityEditor;
using Utilities;

public class EditorMenu : EditorWindow
{
    [MenuItem("CustomTools/BoardAdjuster")]
    public static void ShowWindow()
    {
        GetWindow<EditorMenu>("BoardAdjusterSettings");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Admin Settings", EditorStyles.boldLabel);
        BoardAdjusterSettings.adminCreateItemTouch = EditorGUILayout.Toggle("Admin Create Item Touch", BoardAdjusterSettings.adminCreateItemTouch);
        BoardAdjusterSettings.itemType = (ItemTypeEnum)EditorGUILayout.EnumPopup("Item Type", BoardAdjusterSettings.itemType);
        BoardAdjusterSettings.matchType = (MatchTypeEnum)EditorGUILayout.EnumPopup("Match Type", BoardAdjusterSettings.matchType);
    }
}
#endif
