using Enums;
using Gameplay;

public class StoneItemView : ItemView
{
    public override void Init(BoardView boardView, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.StoneItem;
        base.Init(boardView, matchType);
    }

    public override void Execute(CellView currentCellView, ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            OnItemExecute?.Invoke(ItemType);
            Destroy(gameObject);
        }
    }
}
