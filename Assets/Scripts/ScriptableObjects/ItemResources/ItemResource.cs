using Enums;
using Gameplay;
using UnityEngine;

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
