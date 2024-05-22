using Enums;
using Gameplay;
using Helper;
using Singleton;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private Transform itemHolderTransform;
    public ItemView CreateItem(ItemTypeEnum itemType)
    {
        var itemResource = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(itemType);
        return Instantiate(itemResource.ItemPrefab, itemHolderTransform);
    }
}
