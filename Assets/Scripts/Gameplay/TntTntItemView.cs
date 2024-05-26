using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using UnityEngine;

public class TntTntItemView : ComboItemView
{
    private int _perimeter = 3;
    public override void Init(BoardView boardView, ExecutionManager executionManager, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.TntTntItem;
        base.Init(boardView, executionManager, matchType);
    }

    public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType)
    {
        if (IsDestinedToDie) return;
        IsDestinedToDie = true;

        var cellsToExecute = _boardView.GetCellViews(cellView =>
        {
            int deltaX = Math.Abs(cellView.X - currentCellView.X);
            int deltaY = Math.Abs(cellView.Y - currentCellView.Y);
            return deltaX <= _perimeter && deltaY <= _perimeter && !(deltaX == 0 && deltaY == 0);
        });

        _executionManager.ExecuteCellViews(currentCellView, cellsToExecute, executeType);
    }
}
