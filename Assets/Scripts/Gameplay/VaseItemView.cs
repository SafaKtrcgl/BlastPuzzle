using Enums;

public class VaseItemView : ObstacleItemView
{
    public override void Init(MatchTypeEnum matchType)
    {
        IsFallable = true;
        ItemType = ItemTypeEnum.VaseItem;
        MatchType = matchType;
    }
    public override void Execute(ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            OnItemExecute?.Invoke();
            Destroy(gameObject);
        }
    }

    public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            OnItemExecute?.Invoke();
            Destroy(gameObject);
        }
    }
}