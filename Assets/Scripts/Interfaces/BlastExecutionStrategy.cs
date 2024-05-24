using DG.Tweening;
using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastExecutionStrategy : IExecutionStrategy
{
    public IEnumerator Execute(CellView tappedCell, HashSet<CellView> cellsToExecute)
    {
        foreach (var cellView in cellsToExecute)
        {
            cellView?.Execute(ExecuteTypeEnum.Blast);
        }

        yield break;
    }
}
