using Enums;
using Helper;
using Singleton;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public abstract class ItemView : MonoBehaviour
    {
        [NonSerialized] public bool IsMatchable = false;
        [NonSerialized] public bool IsFallable = false;

        [SerializeField] protected Image mainImage;

        public virtual bool IsSpecial { get; set; } = false;

        public Action<ItemTypeEnum> OnItemExecute;
        public ItemTypeEnum ItemType { get; protected set; }
        public MatchTypeEnum MatchType { get; protected set; }
        public abstract void Execute(ExecuteTypeEnum executeType);
        public abstract void OnNeighbourExecute(ExecuteTypeEnum executeType);

        public virtual void Init(MatchTypeEnum matchType)
        {
            MatchType = matchType;
            mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(0);
        }
    }
}
