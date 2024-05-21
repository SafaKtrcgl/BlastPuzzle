using Enums;
using Gameplay;
using UnityEngine;

[CreateAssetMenu]
public class ItemResource : ScriptableObject
{
    [SerializeField] private ItemTypeEnum itemType;
    [SerializeField] private ItemView itemPrefab;

    public ItemTypeEnum ItemType => itemType;
    public ItemView ItemPrefab => itemPrefab;
}
