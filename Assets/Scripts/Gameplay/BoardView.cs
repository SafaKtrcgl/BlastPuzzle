using Gameplay;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private ItemFactory itemFactory;
    [SerializeField] private CellView cellViewPrefab;
    [SerializeField] private RectTransform boardBackgroundRecttransform;

    private int _width;
    public int Width => _width;

    private int _height;
    public int Height => _height;

    private readonly float BackgroundWidthPadding = 35f;
    private readonly float BackgroundHeightPadding = 50f;

    public void Init(int width, int height, string[] content)
    {
        _width = width;
        _height = height;

        ConstructBoard(content);
    }

    private void ConstructBoard(string[] content)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var anchoredPos = new Vector2((x - ((Width - 1) / 2f)) * CellView.CellSize, (y - ((Height - 1) / 2f)) * CellView.CellSize);

                var cellView = Instantiate(cellViewPrefab, transform);
                cellView.rectTransform.anchoredPosition = anchoredPos;
                cellView.Init(this, x, y);

                var index = y * Width + x;
                var itemView = itemFactory.CreateItem(ItemDataParser.GetItemType(content[index]));
                itemView.rectTransform.anchoredPosition = anchoredPos;
                itemView.Init(ItemDataParser.GetMatchType(content[index]));
            }
        }

        boardBackgroundRecttransform.sizeDelta = new Vector2(Width * CellView.CellSize + BackgroundWidthPadding, Height * CellView.CellSize + BackgroundHeightPadding);
    }
}
