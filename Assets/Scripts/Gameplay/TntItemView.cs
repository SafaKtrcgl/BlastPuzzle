using Enums;
using Gameplay;
using System;

public class TntItemView : ItemView
{
    public override void Init(BoardView boardView, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.TntItem;
        base.Init(boardView, matchType);
    }

    public override void Execute(CellView currentCellView, ExecuteTypeEnum executeType)
    {
        if (IsDestinedToDie) return;
        IsDestinedToDie = true;

        var cellsToExecute = _boardView.GetCellViews(cellView =>
        {
            int deltaX = Math.Abs(cellView.X - currentCellView.X);
            int deltaY = Math.Abs(cellView.Y - currentCellView.Y);
            return deltaX <= 2 && deltaY <= 2 && !(deltaX == 0 && deltaY == 0);
        });

        _boardView.ExecuteCellViews(currentCellView, cellsToExecute, executeType);
    }

    public override bool TryInteract(CellView currentCellView)
    {
        var cellsToExecute = MatchFinder.FindMatchCluster(currentCellView);

        if (cellsToExecute.Count >= Config.TntTnTMinimumRequiredMatch)
        {
            _boardView.ExecuteCellViews(currentCellView, cellsToExecute, ExecuteTypeEnum.Combo);
        }
        else
        {
            Execute(currentCellView, ExecuteTypeEnum.Special);
        }

        return true;
    }
}
