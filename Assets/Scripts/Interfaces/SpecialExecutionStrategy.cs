using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;

public class SpecialExecutionStrategy : IExecutionStrategy
{
    public IEnumerator Execute(CellView tappedCell, HashSet<CellView> cellsToExecute)
    {
        foreach (var cellView in cellsToExecute)
        {
            cellView?.Execute(ExecuteTypeEnum.Special);
        }

        yield break;
    }
}
