using DG.Tweening;
using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialExecutionStrategy : IExecutionStrategy
{
    private bool _isRunning = true;
    public IEnumerator Execute(CellView tappedCell, HashSet<CellView> cellsToExecute)
    {
        var itemView = tappedCell.ItemInside;

        itemView.transform.DOScale(Vector3.one * 1.2f, 5f).OnComplete(() =>
        {
            foreach (var cellView in cellsToExecute)
            {
                cellView?.Execute(ExecuteTypeEnum.Special);
            }

            Debug.Log("Destroyed");
            itemView.DestroyItem();

            _isRunning = false;
        });

        yield return new WaitWhile(() => _isRunning);
    }
}
