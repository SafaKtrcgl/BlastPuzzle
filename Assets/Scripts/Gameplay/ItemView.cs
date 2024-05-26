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
        protected ExecutionManager _executionManager;

        public string State { get; protected set; } = "0";

        public bool IsDestinedToDie { protected set; get; }

        public Action<ItemTypeEnum> OnItemExecute;
        public ItemTypeEnum ItemType { get; protected set; }
        public MatchTypeEnum MatchType { get; protected set; }
        
        public abstract void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType);

        public virtual void OnNeighbourExecute(int executionId, ExecuteTypeEnum executeType)
        {

        }

        public virtual bool TryInteract(CellView currentCellView)
        {
            return false;
        }

        public virtual void Init(BoardView boardView, ExecutionManager executionManager, MatchTypeEnum matchType)
        {
            _boardView = boardView;
            _executionManager = executionManager;
            mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(0);
            SetMatchableType(matchType);
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

        public virtual void Highight()
        {

        }

        public virtual void Unhighight()
        {

        }

        public virtual void SetMatchableType(MatchTypeEnum matchType)
        {
            MatchType = matchType;
        }

        public virtual void SetState(string currentState)
        {
            State = currentState;
        }
    }
}
