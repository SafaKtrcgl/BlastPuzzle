using Enums;
using Singleton;
using System.Linq;
using UnityEngine;

namespace Helper
{
    public class ItemResourceHelper : HelperBase
    {
        [SerializeField] private ItemResource[] itemResources;

        public ItemResource TryGetItemResource(ItemTypeEnum itemType)
        {
            return itemResources.FirstOrDefault(x => x.ItemType == itemType);
        }
    }
}
