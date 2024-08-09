using DG.Tweening;
using Gameplay.Items;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gameplay.Managers
{
    public class FallManager : MonoBehaviour
    {
        private BoardView _boardView;
        private readonly float FallDurationPerCell = .175f;
        private readonly Ease FallEase = Ease.Linear;

        public Action OnBoardItemFallEnd;
        public Action OnFillItemFallEnd;

        public void Init(BoardView boardView)
        {
            _boardView = boardView;
        }

        public void HandleBoardItems()
        {
            Sequence boardItemFallSequence = DOTween.Sequence();

            int fallDistance;
            for (int x = 0; x < _boardView.Width; x++)
            {
                fallDistance = 0;
                for (int y = 0; y < _boardView.Height; y++)
                {
                    var cellView = _boardView.GetCellView(x, y);

                    if (cellView.ItemInside == null)
                    {
                        fallDistance++;
                    }
                    else if (!cellView.ItemInside.IsFallable)
                    {
                        fallDistance = 0;
                    }
                    else if (fallDistance > 0)
                    {
                        boardItemFallSequence.Join(HandleItemFall(x, y, fallDistance));
                    }
                }
            }
            boardItemFallSequence.OnComplete(() =>
            {
                OnBoardItemFallEnd?.Invoke();
            });
        }

        private Tween HandleItemFall(int fromX, int fromY, int fallDistance)
        {
            var cellViewToFall = _boardView.GetCellView(fromX, fromY - fallDistance);
            var fallItem = _boardView.GetCellView(fromX, fromY).ExtractItem();
            cellViewToFall.InsertItem(fallItem);
            return ((RectTransform)fallItem.transform).DOAnchorPos(((RectTransform)cellViewToFall.transform).anchoredPosition, FallDurationPerCell * math.sqrt(fallDistance)).SetEase(FallEase);
        }

        public Sequence HandleFillItemFall(List<ItemView> itemsToPlace, int fromX)
        {
            Sequence fillItemFallSequence = DOTween.Sequence();
            var fallDistance = itemsToPlace.Count;

            for (int i = 0; i < itemsToPlace.Count; i++)
            {
                var fallItem = itemsToPlace[i];
                var toY = (_boardView.Height - 1) - i;
                var cellViewToFall = _boardView.GetCellView(fromX, toY);
                cellViewToFall.InsertItem(fallItem);
                fillItemFallSequence.Join(((RectTransform)fallItem.transform).DOAnchorPos(((RectTransform)cellViewToFall.transform).anchoredPosition, FallDurationPerCell * math.sqrt(fallDistance)).SetEase(FallEase));
            }

            return fillItemFallSequence;
        }
    }
}
