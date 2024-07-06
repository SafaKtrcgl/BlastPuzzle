using Enums;
using Gameplay.Managers;
using Helpers;

namespace Gameplay.Items
{
    public class VaseItemView : ItemView
    {
        private int _currentHp = 2;
        private int _lastAffectedExecutionId = -1;

        public override void Init(BoardView boardView, ExecutionManager executionManager, PoolManager poolManager, MatchTypeEnum matchType)
        {
            IsFallable = true;
            ItemType = ItemTypeEnum.VaseItem;
            base.Init(boardView, executionManager, poolManager, matchType);
        }

        public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType, int executionIndex)
        {
            if (executeType == ExecuteTypeEnum.Special)
            {
                TakeHit(executeType);
            }
        }

        public override void OnNeighbourExecute(int executionId, ExecuteTypeEnum executeType)
        {
            if (executeType == ExecuteTypeEnum.Blast || executeType == ExecuteTypeEnum.Merge)
            {
                if (_lastAffectedExecutionId == executionId) return;
                _lastAffectedExecutionId = executionId;

                TakeHit(executeType);
            }
        }

        private void TakeHit(ExecuteTypeEnum executeType)
        {
            switch (_currentHp)
            {
                case 2:
                    SetState(1);
                    break;

                case 1:
                    DestroyItem(executeType);
                    break;
            }
        }

        public override void SetState(int state)
        {
            State = state;
            if (State == 1)
            {
                SetStateBroken();
            }
        }

        private void SetStateBroken()
        {
            mainSprite.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(1);

            _currentHp = 1;
        }
    }
}
