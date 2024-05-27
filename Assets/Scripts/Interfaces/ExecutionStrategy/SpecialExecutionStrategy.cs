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
        Sequence executionSequence = DOTween.Sequence();

        var itemView = tappedCell.ItemInside;

        itemView.transform.SetAsLastSibling();

        executionSequence.Append(itemView.transform.DOScale(Vector3.one * 1.2f, .25f).SetEase(Ease.OutBack));
        executionSequence.Append(itemView.transform.DOPunchRotation(Vector3.forward * 35, .25f).SetEase(Ease.OutSine));

        executionSequence.OnComplete(() =>
        {
            foreach (var cellView in cellsToExecute)
            {
                cellView?.Execute(ExecuteTypeEnum.Special);
            }

            itemView.DestroyItem();

            _isRunning = false;
        });

        yield return new WaitWhile(() => _isRunning);
    }
}
