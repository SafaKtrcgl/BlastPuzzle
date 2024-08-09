using Enums;
using Gameplay.Managers;

namespace Gameplay.Items
{
    public class StoneItemView : ItemView
    {
        public override void Init(BoardView boardView, ExecutionManager executionManager, MatchTypeEnum matchType)
        {
            ItemType = ItemTypeEnum.StoneItem;
            base.Init(boardView, executionManager, matchType);
        }

        public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType, int executionIndex)
        {
            if (executeType == ExecuteTypeEnum.Special)
            {
                DestroyItem(executeType);
            }
        }
    }
}
