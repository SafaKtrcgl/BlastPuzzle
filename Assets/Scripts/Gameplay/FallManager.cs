using DG.Tweening;
using Gameplay;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    private BoardView _boardView;
    private readonly float FallDurationPerCell = .175f;
    private readonly Ease FallEase = Ease.Linear;

    public void Init(BoardView boardView)
    {
        _boardView = boardView;
    }

    public void HandleBoardItems()
    {
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
                else
                {
                    HandleItemFall(x, y, fallDistance);
                }
            }
        }
    }

    public void HandleFillItems(List<ItemView> itemsToPlace, int x)
    {
        if (itemsToPlace.Count < 1) return;

        for (int i = 0; i < itemsToPlace.Count; i++)
        {
            HandleItemFall(itemsToPlace[i], x, (_boardView.Height - 1) - i, itemsToPlace.Count);
        }
    }

    private void HandleItemFall(int fromX, int fromY, int fallDistance)
    {
        if (fallDistance < 1) return;

        var cellViewToFall = _boardView.GetCellView(fromX, fromY - fallDistance);
        var fallItem = _boardView.GetCellView(fromX, fromY).ExtractItem();
        cellViewToFall.InsertItem(fallItem);
        ((RectTransform)fallItem.transform).DOAnchorPos(((RectTransform)cellViewToFall.transform).anchoredPosition, FallDurationPerCell * math.sqrt(fallDistance)).SetEase(FallEase);
    }

    private void HandleItemFall(ItemView fallItem, int fromX, int toY, int fallDistance)
    {
        var cellViewToFall = _boardView.GetCellView(fromX, toY);
        cellViewToFall.InsertItem(fallItem);
        ((RectTransform)fallItem.transform).DOAnchorPos(((RectTransform)cellViewToFall.transform).anchoredPosition, FallDurationPerCell * math.sqrt(fallDistance)).SetEase(FallEase);
    }
}
