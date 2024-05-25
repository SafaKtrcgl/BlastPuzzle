using Enums;
using Gameplay;

public class VaseItemView : ItemView
{
    public override void Init(BoardView boardView, ExecutionManager executionManager, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.VaseItem;
        base.Init(boardView, executionManager, matchType);
    }
    public override void Execute(CellView currentCellView, ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            DestroyItem();
        }
    }

    public override void OnNeighbourExecute(ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Blast || executeType == ExecuteTypeEnum.Merge)
        {
            DestroyItem();
        }
    }
}