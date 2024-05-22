using DG.Tweening;
using Gameplay;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private ItemFactory itemFactory;
    [SerializeField] private InputListener inputListener;
    [SerializeField] private CellView cellViewPrefab;
    [SerializeField] private RectTransform boardBackgroundRecttransform;

    private int _width;
    public int Width => _width;

    private int _height;
    public int Height => _height;

    private readonly float BackgroundWidthPadding = 35f;
    private readonly float BackgroundHeightPadding = 50f;

    private CellView[] _cellViews;
    public CellView GetCellView(int x, int y) => _cellViews[y * Width + x];

    private bool _isBussy = true;
    public bool IsBussy => _isBussy;

    public void Init(int width, int height, string[] content)
    {
        _width = width;
        _height = height;

        _cellViews = new CellView[_width * _height];

        ConstructBoard(content);

        ((RectTransform)transform).DOAnchorPosX(0, .5f).SetEase(Ease.OutBack).OnComplete(() => _isBussy = false);
    }

    private void ConstructBoard(string[] content)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var anchoredPos = new Vector2((x - ((Width - 1) / 2f)) * CellView.CellSize, (y - ((Height - 1) / 2f)) * CellView.CellSize);

                var cellView = Instantiate(cellViewPrefab, transform);
                ((RectTransform)cellView.transform).anchoredPosition = anchoredPos;
                cellView.Init(this, x, y);

                inputListener.Init(this);
                cellView.OnClickAction += inputListener.OnCellTap;

                var index = y * Width + x;
                _cellViews[index] = cellView;

                var itemView = itemFactory.CreateItem(ItemDataParser.GetItemType(content[index]));
                ((RectTransform)itemView.transform).anchoredPosition = anchoredPos;
                itemView.Init(ItemDataParser.GetMatchType(content[index]));

                cellView.SetItemInside(itemView);
            }
        }

        AssignCellNeighbours();

        boardBackgroundRecttransform.sizeDelta = new Vector2(Width * CellView.CellSize + BackgroundWidthPadding, Height * CellView.CellSize + BackgroundHeightPadding);
    }

    private void AssignCellNeighbours()
    {
        foreach (CellView cellView in _cellViews)
        {
            if (cellView.X != Width - 1)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Right, _cellViews[cellView.Y * Width + cellView.X + 1]);
            }
            if (cellView.X != 0)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Left, _cellViews[cellView.Y * Width + cellView.X - 1]);
            }
            if (cellView.Y != Height - 1)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Up, _cellViews[(cellView.Y + 1) * Width + cellView.X]);
            }
            if (cellView.Y != 0)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Down, _cellViews[(cellView.Y - 1) * Width + cellView.X]);
            }
        }
    }

    public void ExecuteCells(List<CellView> cellsToExecute)
    {
        foreach (var cellView in cellsToExecute)
        {
            cellView.Execute();
        }
    }
}
