using DG.Tweening;
using Enums;
using Gameplay;
using Gameplay.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces.Strategy
{
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
                var itemInside = cellView.ItemInside;
                if (itemInside == null) continue;

                itemInside.SetSpriteSortingLayer("SpecialItem");
                executeSequence.Join(((RectTransform)itemInside.transform).DOAnchorPos(mergePos, .35f));
                executeSequence.Join(itemInside.transform.DOScale(.2f, .35f).SetEase(Ease.InSine));
            }

            executeSequence.AppendCallback(() =>
            {
                foreach (var cellView in cellsToExecute)
                {
                    cellView.ItemInside.DestroyItem(ExecuteTypeEnum.Combo);
                }

                var itemView = _itemFactory.CreateItem(ItemTypeEnum.TntTntItem, MatchTypeEnum.None);
                ((RectTransform)itemView.transform).anchoredPosition = ((RectTransform)tappedCell.transform).anchoredPosition;
                itemView.transform.localScale = Vector3.one * .2f;

                tappedCell.InsertItem(itemView);

                itemView.transform.DOScale(Vector3.one, .25f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    tappedCell.ItemInside.Execute(GameplayInputController.MoveCount, tappedCell, ExecuteTypeEnum.Special);
                    _isRunning = false;
                });
            });

            yield return new WaitWhile(() => _isRunning);
        }
    }
}
