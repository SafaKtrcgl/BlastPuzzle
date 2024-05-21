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
        cubeImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).GetCubeSprite(matchType);
    }

    public override void OnInteract()
    {
        
    }
}
