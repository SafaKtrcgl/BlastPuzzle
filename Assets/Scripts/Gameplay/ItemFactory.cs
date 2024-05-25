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

    public void Init(BoardView boardView, ExecutionManager executionManager)
    {
        _boardView = boardView;
        _executionManager = executionManager;
    }

    public ItemView CreateItem(ItemTypeEnum itemType, MatchTypeEnum matchType)
    {
        var itemResource = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(itemType);

        var itemView = Instantiate(itemResource.ItemPrefab, itemHolderTransform);
        itemView.Init(_boardView, _executionManager, matchType);

        if (itemType.IsObstacle()) OnObstacleItemCreated?.Invoke(itemType);
        
        return itemView;
    }
}
