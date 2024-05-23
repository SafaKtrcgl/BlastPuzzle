using Enums;

public class VaseItemView : ObstacleItemView
{
    public override void Init(MatchTypeEnum matchType)
    {
        IsFallable = true;
        ItemType = ItemTypeEnum.VaseItem;
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
        if (executeType == ExecuteTypeEnum.Blast || executeType == ExecuteTypeEnum.Merge)
        {
            OnItemExecute?.Invoke(ItemType);
            Destroy(gameObject);
        }
    }
}