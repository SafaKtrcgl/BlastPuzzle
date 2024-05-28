using Enums;
using UnityEngine;

[CreateAssetMenu()]
public class EditorSettings : ScriptableObject
{
    public bool adminCreateItemTouch;
    public ItemTypeEnum itemType;
    public MatchTypeEnum matchType;
}
