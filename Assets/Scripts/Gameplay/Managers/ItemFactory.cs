using Enums;
using Extensions;
using Gameplay.Items;
using Helpers;
using ScriptableObjects;
using System;
using UnityEngine;

namespace Gameplay.Managers
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] private Transform itemHolderTransform;

        public Action<ItemTypeEnum> OnObstacleItemCreated;

        private BoardView _boardView;
        private ExecutionManager _executionManager;

        public void Init(BoardView boardView, ExecutionManager executionManager)
        {
            _boardView = boardView;
            _executionManager = executionManager;
        }

        public ItemView CreateItem(ItemTypeEnum itemType, MatchTypeEnum matchType)
        {
            var itemResource = HelperResources.Instance
                .GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper)
                .TryGetItemResource(itemType);

            ItemView itemView = GetOrCreateItemView(itemType, itemResource);

            itemView.Init(_boardView, _executionManager, matchType);

            if (itemType.IsObstacle())
            {
                OnObstacleItemCreated?.Invoke(itemType);
            }

            return itemView;
        }

        private ItemView GetOrCreateItemView(ItemTypeEnum itemType, ItemResource itemResource)
        {
            var recyclableType = itemType.GetRecyclableType();
            if (recyclableType != RecyclableTypeEnum.None)
            {
                var itemView = PoolManager.Instance.GetFromPool<ItemView>(recyclableType);
                if (itemView != null)
                {
                    itemView.transform.SetParent(itemHolderTransform);
                    return itemView;
                }
            }

            return Instantiate(itemResource.ItemPrefab, itemHolderTransform);
        }
    }
}
