using Enums;
using Gameplay.Managers;
using Utilities;

namespace Gameplay.Items
{
    public class TntItemView : ItemView
    {
        private int _perimeter = 2;

        public override void Init(BoardView boardView, ExecutionManager executionManager, MatchTypeEnum matchType)
        {
            IsFallable = true;
            ItemType = ItemTypeEnum.TntItem;
            base.Init(boardView, executionManager, matchType);
        }

        public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType, int executionIndex)
        {
            if (IsDestinedToDie) return;
            IsDestinedToDie = true;

            _executionManager.ExecuteCellViews(currentCellView, _boardView.GetCellViewsInPerimeter(currentCellView, _perimeter), executeType, executionIndex + 1);
        }

        public override bool TryInteract(CellView currentCellView)
        {
            var cellsToExecute = _boardView.GetMatchClusterFromCellView(currentCellView);

            if (cellsToExecute.Count >= Config.TntTnTMinimumRequiredMatch)
            {
                _executionManager.ExecuteCellViews(currentCellView, cellsToExecute, ExecuteTypeEnum.Combo, 0);
            }
            else
            {
                Execute(GameplayInputController.MoveCount, currentCellView, ExecuteTypeEnum.Special, 0);
            }

            return true;
        }

        public override void DestroyItem(ExecuteTypeEnum executeType)
        {
            OnItemExecute?.Invoke(ItemType);
            mainSprite.enabled = false;
            if (executeType == ExecuteTypeEnum.Combo)
            {
                OnDestroyParticleEnd();
            }
            else
            {
                PlayDestroyParticles();
            }
        }
    }
}
