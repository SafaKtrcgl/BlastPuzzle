using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private Transform itemHolderTransform;

    public Action<ItemTypeEnum> OnObstacleItemCreated;

    private BoardView _boardView;
    private ExecutionManager _executionManager;
    private PoolManager _poolManager;

    public void Init(BoardView boardView, ExecutionManager executionManager, PoolManager poolManager)
    {
        _boardView = boardView;
        _executionManager = executionManager;
        _poolManager = poolManager;
    }

    public ItemView CreateItem(ItemTypeEnum itemType, MatchTypeEnum matchType)
    {
        var itemResource = HelperResources.Instance
            .GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper)
            .TryGetItemResource(itemType);

        ItemView itemView = GetOrCreateItemView(itemType, itemResource);

        itemView.Init(_boardView, _executionManager, _poolManager, matchType);

        if (itemType.IsObstacle())
        {
            OnObstacleItemCreated?.Invoke(itemType);
        }

        return itemView;
    }

    private ItemView GetOrCreateItemView(ItemTypeEnum itemType, ItemResource itemResource)
    {
        if (itemType.IsRecyclable())
        {
            var itemView = _poolManager.GetFromPool<ItemView>(itemType);
            if (itemView != null)
            {
                itemView.transform.SetParent(itemHolderTransform);
                return itemView;    
            }
        }

        return Instantiate(itemResource.ItemPrefab, itemHolderTransform);
    }
}
