using DG.Tweening;
using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeExecutionStrategy : IExecutionStrategy
{
    private readonly ItemFactory _itemFactory;
    private bool _isRunning = true;

    public MergeExecutionStrategy(ItemFactory itemFactory)
    {
        _itemFactory = itemFactory;
    }

    public IEnumerator Execute(CellView tappedCell, HashSet<CellView> cellsToExecute)
    {
        Sequence executeSequence = DOTween.Sequence();

        var mergePos = ((RectTransform)tappedCell.transform).anchoredPosition;

        foreach (var cellView in cellsToExecute)
        {
            if (cellView != tappedCell)
            {
                executeSequence.Join(((RectTransform)cellView.ItemInside.transform).DOAnchorPos(mergePos, .35f));
            }
        }

        executeSequence.OnComplete(() =>
        {
            _isRunning = false;
            foreach (var cellView in cellsToExecute)
            {
                cellView?.Execute(ExecuteTypeEnum.Merge);
            }

            var itemView = _itemFactory.CreateItem(ItemTypeEnum.TntItem, MatchTypeEnum.Special);
            ((RectTransform)itemView.transform).anchoredPosition = ((RectTransform)tappedCell.transform).anchoredPosition;

            tappedCell.InsertItem(itemView);
        });

        yield return new WaitWhile(() => _isRunning);
    }
}
