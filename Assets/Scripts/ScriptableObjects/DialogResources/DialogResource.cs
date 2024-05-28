using Enums;
using UI.Dialog;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class DialogResource : ScriptableObject
    {
        [SerializeField] private DialogTypeEnum dialogType;
        [SerializeField] private DialogView dialogPrefab;

        public DialogTypeEnum DialogType => dialogType;
        public DialogView DialogPrefab => dialogPrefab;
    }
}
