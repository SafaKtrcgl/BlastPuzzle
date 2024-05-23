using DG.Tweening;
using Enums;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeExecutionStrategy : IExecutionStrategy
{
    private readonly ItemFactory itemFactory;

    public MergeExecutionStrategy(ItemFactory itemFactory)
    {
        this.itemFactory = itemFactory;
    }

    public IEnumerator Execute(CellView tappedCell, List<CellView> cellsToExecute, ItemTypeEnum itemType)
    {
        Sequence executeSequence = DOTween.Sequence();

        executeSequence.OnComplete(() =>
        {
            foreach (var cellView in cellsToExecute)
            {
                cellView?.Execute(ExecuteTypeEnum.Merge);
            }

            var itemView = itemFactory.CreateItem(itemType);
            ((RectTransform)itemView.transform).anchoredPosition = ((RectTransform)tappedCell.transform).anchoredPosition;
            itemView.Init(MatchTypeEnum.Combo);

            tappedCell.InsertItem(itemView);
        });

        var mergePos = ((RectTransform)tappedCell.transform).anchoredPosition;

        foreach (var cellView in cellsToExecute)
        {
            if (cellView != tappedCell)
            {
                executeSequence.Join(((RectTransform)cellView.ItemInside.transform).DOAnchorPos(mergePos, .35f));
            }
        }

        yield return new WaitWhile(() => executeSequence.IsPlaying());
    }
}
