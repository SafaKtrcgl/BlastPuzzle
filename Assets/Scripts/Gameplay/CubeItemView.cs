using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CubeItemView : ItemView
{
    [SerializeField] Image cubeImage;
    public override void Init(MatchTypeEnum matchType)
    {
        IsMatchable = true;
        IsFallable = true;
        ItemType = ItemTypeEnum.CubeItem;
        MatchType = matchType;

        cubeImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(Array.IndexOf(ItemDataParser.cubeItemTypes, MatchType));
    }

    public override void Execute(ExecuteTypeEnum executeType)
    {
        OnItemExecute?.Invoke();
        Destroy(gameObject);
    }

    public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
    {

    }
}
