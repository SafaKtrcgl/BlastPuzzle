using Enums;
using Gameplay;
using System.Collections.Generic;
using System;
using System.Collections;

public class SpecialExecutionStrategy : IExecutionStrategy
{
    private readonly Func<CellView, int, List<CellView>> getCellsInPerimeter;

    public SpecialExecutionStrategy(Func<CellView, int, List<CellView>> getCellsInPerimeter)
    {
        this.getCellsInPerimeter = getCellsInPerimeter;
    }

    public IEnumerator Execute(CellView tappedCell, List<CellView> cellsToExecute, ItemTypeEnum itemType)
    {
        if (itemType == ItemTypeEnum.TntItem)
        {
            var executeCells = getCellsInPerimeter(tappedCell, 2);
            foreach (var cellView in executeCells)
            {
                cellView?.Execute(ExecuteTypeEnum.Special);
            }
        }
        yield break;
    }
}
