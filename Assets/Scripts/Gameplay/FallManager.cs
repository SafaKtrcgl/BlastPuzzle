using DG.Tweening;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    private BoardView _boardView;

    public void Init(BoardView boardView)
    {
        _boardView = boardView;
    }

    public void SettleBoard()
    {
        int dropCount;
        for (int x = 0; x < _boardView.Width; x++)
        {
            dropCount = 0;
            for (int y = 0; y < _boardView.Height; y++)
            {
                var cellView = _boardView.GetCellView(x, y);

                if (cellView.ItemInside == null)
                {
                    dropCount++;
                }
                else if (!cellView.ItemInside.IsFallable)
                {
                    dropCount = 0;
                }
                else
                {
                    if (dropCount > 0)
                    {
                        var cellViewToFall = _boardView.GetCellView(x, y - dropCount);
                        var fallItem = cellView.ExtractItem();
                        ((RectTransform)fallItem.transform).DOAnchorPos(((RectTransform)cellViewToFall.transform).anchoredPosition, .5f).OnComplete(() => cellViewToFall.InsertItem(fallItem));
                    }
                }
            }
        }
    }
}
