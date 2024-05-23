using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public Action<ItemTypeEnum> OnObstacleItemCreated;

    [SerializeField] private Transform itemHolderTransform;
    public ItemView CreateItem(ItemTypeEnum itemType, MatchTypeEnum matchType)
    {
        var itemResource = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(itemType);

        var itemView = Instantiate(itemResource.ItemPrefab, itemHolderTransform);
        itemView.Init(matchType);

        if (itemType.IsObstacle()) OnObstacleItemCreated?.Invoke(itemType);
        
        return itemView;
    }
}
