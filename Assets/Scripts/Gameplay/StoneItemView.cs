using Enums;
using Gameplay;

public class StoneItemView : ItemView
{
    public override void Init(BoardView boardView, ExecutionManager executionManager, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.StoneItem;
        base.Init(boardView, executionManager, matchType);
    }

    public override void Execute(CellView currentCellView, ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            DestroyItem();
        }
    }
}
