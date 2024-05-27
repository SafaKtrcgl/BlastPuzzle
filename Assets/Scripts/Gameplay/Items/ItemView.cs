using Enums;
using Helper;
using Singleton;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public abstract class ItemView : MonoBehaviour, IRecyclable
    {
        [SerializeField] protected Image mainImage;
        [SerializeField] protected ParticleSystem destroyParticleSystem;
        [SerializeField] protected ParticleStopCallback destroyParticleCallback;

        protected BoardView _boardView;
        protected ExecutionManager _executionManager;
        protected PoolManager _poolManager;

        public string State { get; protected set; } = "0";

        public bool IsDestinedToDie { protected set; get; }

        public Action<ItemTypeEnum> OnItemExecute;
        public ItemTypeEnum ItemType { get; protected set; }
        public MatchTypeEnum MatchType { get; protected set; }
        public GameObject RecyclableGameObject { get; set; }
        public abstract void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType);

        public virtual void OnNeighbourExecute(int executionId, ExecuteTypeEnum executeType)
        {

        }

        public virtual bool TryInteract(CellView currentCellView)
        {
            return false;
        }

        public virtual void Init(BoardView boardView, ExecutionManager executionManager, PoolManager poolManager, MatchTypeEnum matchType)
        {
            _boardView = boardView;
            _executionManager = executionManager;
            _poolManager = poolManager;
            mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(0);
            mainImage.SetNativeSize();
            ((RectTransform)mainImage.transform).sizeDelta /= 2f;
            SetMatchableType(matchType);
            RecyclableGameObject = gameObject;

            destroyParticleCallback.OnParticleStopAction += OnDestroyParticleEnd;
        }

        public virtual void DestroyItem()
        {
            PlayDestroyParticles();
            OnItemExecute?.Invoke(ItemType);
            mainImage.enabled = false;
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

        public virtual void SetState(string currentState)
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
            Recycle();
        }

        public virtual void Recycle()
        {
            Destroy(gameObject);
        }
    }
}
