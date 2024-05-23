using Enums;

namespace Gameplay
{
    public class BoxItemView : ItemView
    {
        public override void Init(BoardView boardView, MatchTypeEnum matchType)
        {
            ItemType = ItemTypeEnum.BoxItem;
            base.Init(boardView, matchType);
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
}
