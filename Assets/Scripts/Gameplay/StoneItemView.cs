using Enums;

public class StoneItemView : ObstacleItemView
{
    public override void Init(MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.StoneItem;
        MatchType = matchType;
    }

    public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            Destroy(gameObject);
        }
    }
}
