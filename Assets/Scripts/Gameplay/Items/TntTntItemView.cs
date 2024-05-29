using Enums;
using Gameplay.Managers;
using System;

namespace Gameplay.Items
{
    public class TntTntItemView : ComboItemView
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

            var cellsToExecute = _boardView.GetCellViews(cellView =>
            {
                int deltaX = Math.Abs(cellView.X - currentCellView.X);
                int deltaY = Math.Abs(cellView.Y - currentCellView.Y);
                return deltaX <= _perimeter && deltaY <= _perimeter && !(deltaX == 0 && deltaY == 0);
            });

            _executionManager.ExecuteCellViews(currentCellView, cellsToExecute, executeType, executionIndex);
        }
    }
}
