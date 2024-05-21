using Enums;
using UnityEngine;

namespace Gameplay
{
    public abstract class ItemView : MonoBehaviour
    {
        [SerializeField] public RectTransform rectTransform;

        public ItemTypeEnum itemType { get; private set; }
        public abstract void Init(MatchTypeEnum matchType);
        public abstract void OnInteract();

    }
}
