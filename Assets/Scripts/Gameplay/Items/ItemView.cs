using Enums;
using Gameplay.Managers;
using Helpers;
using Interfaces.Recycle;
using System;
using UnityEngine;
using Utilities;

namespace Gameplay.Items
{
    public abstract class ItemView : MonoBehaviour, IRecyclable
    {
        [SerializeField] protected SpriteRenderer mainSprite;
        [SerializeField] protected ParticleSystem destroyParticleSystem;
        [SerializeField] protected ParticleStopCallback destroyParticleCallback;

        protected BoardView _boardView;
        protected ExecutionManager _executionManager;

        public int State { get; protected set; } = 0;

        public bool IsDestinedToDie { protected set; get; }
        public bool IsFallable { protected set; get; }

        public Action<ItemTypeEnum> OnItemExecute;
        public ItemTypeEnum ItemType { get; protected set; }
        public MatchTypeEnum MatchType { get; protected set; }
        public GameObject RecyclableGameObject { get; set; }
        public RecyclableTypeEnum RecyclableType { get; set; }

        public abstract void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType, int executionIndex);

        public override string ToString()
        {
            return ItemDataParser.GetItemKey(ItemType, MatchType) + State;
        }

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
            mainSprite.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(0);

            ((RectTransform)mainSprite.transform).sizeDelta /= 2f;
            SetMatchableType(matchType);
            RecyclableGameObject = gameObject;

            destroyParticleCallback.OnParticleStopAction += OnDestroyParticleEnd;
        }

        public virtual void DestroyItem(ExecuteTypeEnum executeType)
        {
            PlayDestroyParticles();
            OnItemExecute?.Invoke(ItemType);
            mainSprite.enabled = false;
        }

        private void OnDisable()
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

        public virtual void SetState(int currentState)
        {
            State = currentState;
        }

        public virtual void PlayDestroyParticles()
        {
            destroyParticleSystem.gameObject.SetActive(true);
        }

        public virtual void OnDestroyParticleEnd()
        {
            destroyParticleCallback.OnParticleStopAction -= OnDestroyParticleEnd;
            PoolManager.Instance.SendToPool(this, RecyclableType);
        }

        public virtual void Recycle()
        {
            Destroy(gameObject);
        }

        public void SetSpriteSortingLayer(string layer)
        {
            mainSprite.sortingLayerName = layer;
        }

        public void SetSpriteSortingOrder(int order)
        {
            mainSprite.sortingOrder = order;
        }
    }
}
