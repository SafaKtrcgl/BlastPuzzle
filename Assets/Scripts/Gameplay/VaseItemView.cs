using Enums;

public class VaseItemView : ObstacleItemView
{
    public override void Init(MatchTypeEnum matchType)
    {
        IsFallable = true;
        ItemType = ItemTypeEnum.VaseItem;
        MatchType = matchType;
    }

    public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
    {

    }
}