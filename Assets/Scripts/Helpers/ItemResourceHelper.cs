using Enums;
using Singleton;
using System.Linq;
using UnityEngine;

namespace Helper
{
    public class ItemResourceHelper : HelperBase
    {
        [SerializeField] private ItemResource[] itemResources;
        [SerializeField] private Sprite[] cubeSprites;

        public ItemResource TryGetItemResource(ItemTypeEnum itemType)
        {
            return itemResources.FirstOrDefault(x => x.ItemType == itemType);
        }

        public Sprite GetCubeSprite(MatchTypeEnum matchType)
        {
            return cubeSprites[(int)matchType - 1];
        }
    }
}
