using DG.Tweening;
using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastExecutionStrategy : IExecutionStrategy
{
    private bool _isRunning = true;

    public IEnumerator Execute(CellView tappedCell, HashSet<CellView> cellsToExecute)
    {
        Sequence blastItemSquishSequence = DOTween.Sequence();

        foreach (var cellView in cellsToExecute)
        {
            blastItemSquishSequence.Join(cellView.ItemInside.transform.DOScale(Vector3.one * .4f, .25f));
        }

        blastItemSquishSequence.OnComplete(() =>
        {
            foreach (var cellView in cellsToExecute)
            {
                cellView?.Execute(ExecuteTypeEnum.Blast);
            }
            _isRunning = false;
        });

        yield return new WaitWhile(() => _isRunning);
    }
}
