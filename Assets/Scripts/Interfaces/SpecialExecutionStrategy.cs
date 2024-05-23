using Enums;
using Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;

public class SpecialExecutionStrategy : IExecutionStrategy
{
    private readonly BoardView _boardView;

    public SpecialExecutionStrategy(BoardView boardView)
    {
        _boardView = boardView;
    }

    public IEnumerator Execute(CellView tappedCell, List<CellView> cellsToExecute, ItemTypeEnum itemType)
    {
        if (itemType == ItemTypeEnum.TntItem)
        {
            var executeCells = _boardView.GetCellViews(cellView =>
            {
                int deltaX = Math.Abs(cellView.X - tappedCell.X);
                int deltaY = Math.Abs(cellView.Y - tappedCell.Y);
                return deltaX <= 2 && deltaY <= 2 && !(deltaX == 0 && deltaY == 0);
            });

            foreach (var cellView in executeCells)
            {
                cellView?.Execute(ExecuteTypeEnum.Special);
            }
        }
        yield break;
    }
}
