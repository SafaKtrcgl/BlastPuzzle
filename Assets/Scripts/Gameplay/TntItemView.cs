using Enums;
using Gameplay;
using System;
using UnityEngine;

public class TntItemView : ItemView
{
    public override void Init(BoardView boardView, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.TntItem;
        base.Init(boardView, matchType);
    }

    public override void Execute(CellView currentCellView, ExecuteTypeEnum executeType)
    {
        var cellsToExecute = _boardView.GetCellViews(cellView =>
        {
            int deltaX = Math.Abs(cellView.X - currentCellView.X);
            int deltaY = Math.Abs(cellView.Y - currentCellView.Y);
            return deltaX <= 2 && deltaY <= 2 && !(deltaX == 0 && deltaY == 0);
        });

        IsDestinedToDie = true;

        _boardView.ExecuteCellViews(currentCellView, cellsToExecute, executeType);

        DestroyItem();
    }

    public override bool TryInteract(CellView currentCellView)
    {
        var cellsToExecute = MatchFinder.FindMatchCluster(currentCellView);

        if (cellsToExecute.Count >= 2)
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
