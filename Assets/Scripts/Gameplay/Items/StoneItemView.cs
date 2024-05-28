using Enums;
using Gameplay;

public class StoneItemView : ItemView
{
    public override void Init(BoardView boardView, ExecutionManager executionManager, PoolManager poolManager, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.StoneItem;
        base.Init(boardView, executionManager, poolManager, matchType);
    }

    public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            DestroyItem(executeType);
        }
    }
}
