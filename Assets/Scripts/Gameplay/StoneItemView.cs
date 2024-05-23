using Enums;

public class StoneItemView : ObstacleItemView
{
    public override void Init(MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.StoneItem;
        base.Init(matchType);
    }

    public override void Execute(ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            OnItemExecute?.Invoke(ItemType);
            Destroy(gameObject);
        }
    }

    public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
    {
        
    }
}
