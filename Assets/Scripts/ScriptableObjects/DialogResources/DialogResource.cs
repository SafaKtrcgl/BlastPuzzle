using Enums;
using Gameplay;
using Singleton;
using UnityEngine;

[CreateAssetMenu]
public class DialogResource : ScriptableObject
{
    [SerializeField] private DialogTypeEnum dialogType;
    [SerializeField] private DialogView dialogPrefab;

    public DialogTypeEnum DialogType => dialogType;
    public DialogView DialogPrefab => dialogPrefab;
}
