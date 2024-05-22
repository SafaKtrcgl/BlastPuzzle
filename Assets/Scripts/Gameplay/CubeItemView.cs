using Enums;
using Gameplay;
using Helper;
using Singleton;
using UnityEngine;
using UnityEngine.UI;

public class CubeItemView : ItemView
{
    [SerializeField] Image cubeImage;
    public override void Init(MatchTypeEnum matchType)
    {
        IsMatchable = true;
        ItemType = ItemTypeEnum.CubeItem;
        MatchType = matchType;

        cubeImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).GetCubeSprite(matchType);
    }

    public override void Execute()
    {
        Destroy(gameObject);
    }
}
