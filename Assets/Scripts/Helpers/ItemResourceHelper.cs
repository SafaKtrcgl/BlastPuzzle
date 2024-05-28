using Enums;
using ScriptableObjects;
using System.Linq;
using UnityEngine;

namespace Helpers
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
