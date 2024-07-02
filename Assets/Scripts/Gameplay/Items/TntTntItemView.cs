using Enums;
using Gameplay.Managers;

namespace Gameplay.Items
{
    public class TntTntItemView : ItemView
    {
        private int _perimeter = 3;
        public override void Init(BoardView boardView, ExecutionManager executionManager, PoolManager poolManager, MatchTypeEnum matchType)
        {
            ItemType = ItemTypeEnum.TntTntItem;
            base.Init(boardView, executionManager, poolManager, matchType);
        }

        public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType, int executionIndex)
        {
            if (IsDestinedToDie) return;
            IsDestinedToDie = true;

            _executionManager.ExecuteCellViews(currentCellView, _boardView.GetCellViewsInPerimeter(currentCellView, _perimeter), executeType, executionIndex);
        }
    }
}
