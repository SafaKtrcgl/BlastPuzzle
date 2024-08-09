using DG.Tweening;
using Enums;
using Gameplay.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Gameplay.Managers
{
    public class FillManager : MonoBehaviour
    {
        public Action OnBoardFilled;

        private BoardView _boardView;
        private ItemFactory _itemFactory;
        private FallManager _fallManager;

        public readonly static int InitialPositionCellPadding = 2;

        public void Init(BoardView boardView, ItemFactory itemFactory, FallManager fallManager)
        {
            _boardView = boardView;
            _itemFactory = itemFactory;
            _fallManager = fallManager;
        }

        public void FillBoard()
        {
            List<ItemView> fillItems = new();
            Vector2 topCellPosition;

            Sequence fillBoardSequence = DOTween.Sequence();

            for (int x = 0; x < _boardView.Width; x++)
            {
                fillItems.Clear();
                topCellPosition = ((RectTransform)_boardView.GetCellView(x, _boardView.Height - 1).transform).anchoredPosition;

                for (int y = _boardView.Height - 1; y > -1; y--)
                {
                    var cellView = _boardView.GetCellView(x, y);

                    if (cellView.ItemInside == null)
                    {
                        var fillItem = _itemFactory.CreateItem(ItemTypeEnum.CubeItem, (MatchTypeEnum) Random.Range(0, ItemDataParser.cubeItemTypes.Length));

                        var initialPositionoffSet = Vector2.up * ((InitialPositionCellPadding + fillItems.Count) * CellView.CellSize);
                        ((RectTransform)fillItem.transform).anchoredPosition = topCellPosition + initialPositionoffSet;

                        fillItems.Add(fillItem);
                    }
                    else if (!cellView.ItemInside.IsFallable)
                    {
                        break;
                    }
                }

                if (fillItems.Count > 0)
                {
                    fillItems.Reverse();
                    fillBoardSequence.Join(_fallManager.HandleFillItemFall(fillItems, x));
                }
            }

            fillBoardSequence.OnComplete(() => OnBoardFilled?.Invoke());
        }
    }
}
