using DG.Tweening;
using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboExecutionStrategy : IExecutionStrategy
{
    private readonly ItemFactory _itemFactory;

    private bool _isRunning = true;

    public ComboExecutionStrategy(ItemFactory itemFactory)
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
                executeSequence.Join(((RectTransform)cellView.ItemInside?.transform).DOAnchorPos(mergePos, .35f));
            }
        }

        executeSequence.AppendCallback(() =>
        {
            foreach (var cellView in cellsToExecute)
            {
                cellView.ItemInside.DestroyItem();
            }

            var itemView = _itemFactory.CreateItem(ItemTypeEnum.TntTntItem, MatchTypeEnum.None);
            ((RectTransform)itemView.transform).anchoredPosition = ((RectTransform)tappedCell.transform).anchoredPosition;

            tappedCell.InsertItem(itemView);
        });

        executeSequence.OnComplete(() =>
        {
            tappedCell.ItemInside.Execute(tappedCell, ExecuteTypeEnum.Special);
            _isRunning = false;
        });

        yield return new WaitWhile(() => _isRunning);
    }
}
