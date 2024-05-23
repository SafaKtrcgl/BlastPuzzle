using Enums;
using Gameplay;

public class VaseItemView : ItemView
{
    public override void Init(BoardView boardView, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.VaseItem;
        base.Init(boardView, matchType);
    }
    public override void Execute(CellView currentCellView, ExecuteTypeEnum executeType)
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