using Enums;
using Gameplay;
using Helper;
using Singleton;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public ItemView CreateItem(ItemTypeEnum itemType)
    {
        var itemResource = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(itemType);
        return Instantiate(itemResource.ItemPrefab, transform);
    }
    /*
    public T CreateItem<T>(ItemTypeEnum itemType) where T : ItemView
    {
        var itemResource = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(itemType);
        return Instantiate(itemResource.ItemPrefab, transform) as T;
    }
    */
}
