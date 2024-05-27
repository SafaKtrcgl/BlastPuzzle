using Enums;

namespace Gameplay
{
    public class BoxItemView : ItemView
    {
        public override void Init(BoardView boardView, ExecutionManager executionManager, PoolManager poolManager, MatchTypeEnum matchType)
        {
            ItemType = ItemTypeEnum.BoxItem;
            base.Init(boardView, executionManager, poolManager, matchType);
        }

        public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType)
        {
            if (executeType == ExecuteTypeEnum.Special)
            {
                DestroyItem();
            }
        }

        public override void OnNeighbourExecute(int executionId, ExecuteTypeEnum executeType)
        {
            if (executeType == ExecuteTypeEnum.Blast || executeType == ExecuteTypeEnum.Merge)
            {
                DestroyItem();
            }
        }
    }
}
