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
        [SerializeField] protected Image mainImage;

        protected BoardView _boardView;

        public bool IsDestinedToDie { protected set; get; }

        public Action<ItemTypeEnum> OnItemExecute;
        public ItemTypeEnum ItemType { get; protected set; }
        public MatchTypeEnum MatchType { get; protected set; }
        
        public abstract void Execute(CellView currentCellView, ExecuteTypeEnum executeType);

        public virtual void OnNeighbourExecute(ExecuteTypeEnum executeType)
        {

        }

        public virtual bool TryInteract(CellView currentCellView)
        {
            return false;
        }

        public virtual void Init(BoardView boardView, MatchTypeEnum matchType)
        {
            _boardView = boardView;
            MatchType = matchType;
            mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(0);
        }

        public virtual void DestroyItem()
        {
            OnItemExecute?.Invoke(ItemType);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnItemExecute = null;
        }
    }
}
