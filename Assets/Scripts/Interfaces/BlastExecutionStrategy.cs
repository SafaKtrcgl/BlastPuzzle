using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;

public class BlastExecutionStrategy : IExecutionStrategy
{
    public IEnumerator Execute(CellView tappedCell, List<CellView> cellsToExecute, ItemTypeEnum itemType)
    {
        foreach (var cellView in cellsToExecute)
        {
            cellView?.Execute(ExecuteTypeEnum.Blast);
        }
        yield break;
    }
}
