using Enums;
using Gameplay.Items;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class ItemResource : ScriptableObject
    {
        [SerializeField] private ItemTypeEnum itemType;
        [SerializeField] private ItemView itemPrefab;
        [SerializeField] private Sprite[] itemSprites;

        public ItemTypeEnum ItemType => itemType;
        public ItemView ItemPrefab => itemPrefab;
        public Sprite ItemSprite(int x) => itemSprites[x];
    }
}
