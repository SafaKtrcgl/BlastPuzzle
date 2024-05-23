using DG.Tweening;
using Enums;
using Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private ItemFactory itemFactory;
    [SerializeField] private FillManager fillManager;
    [SerializeField] private FallManager fallManager;
    [SerializeField] private CellView cellViewPrefab;
    [SerializeField] private RectTransform boardBackgroundRecttransform;

    public Action<int, int> OnCellTapped;

    private int _width;
    public int Width => _width;

    private int _height;
    public int Height => _height;

    private readonly float BackgroundWidthPadding = 35f;
    private readonly float BackgroundHeightPadding = 50f;

    private CellView[,] _cellViews;
    public CellView GetCellView(int x, int y) => _cellViews[x, y];

    private bool _isBussy = true;
    public bool IsBussy => _isBussy;

    public void Init(int width, int height, string[] content)
    {
        _width = width;
        _height = height;

        _cellViews = new CellView[_width, _height];

        fallManager.Init(this);
        fillManager.Init(this, itemFactory, fallManager);

        ConstructBoard(content);

        ((RectTransform)transform).DOAnchorPosX(0, .5f).SetEase(Ease.OutBack).OnComplete(() => _isBussy = false);
    }

    private void ConstructBoard(string[] content)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var cellView = Instantiate(cellViewPrefab, transform);
                cellView.Init(this, x, y);

                cellView.OnClickAction += OnCellTap;
                _cellViews[x, y] = cellView;
                
                var index = y * Width + x;

                var itemView = itemFactory.CreateItem(ItemDataParser.GetItemType(content[index]));
                ((RectTransform)itemView.transform).anchoredPosition = ((RectTransform)cellView.transform).anchoredPosition;
                itemView.Init(ItemDataParser.GetMatchType(content[index]));

                cellView.InsertItem(itemView);
            }
        }

        AssignCellNeighbours();

        boardBackgroundRecttransform.sizeDelta = new Vector2(Width * CellView.CellSize + BackgroundWidthPadding, Height * CellView.CellSize + BackgroundHeightPadding);
    }

    private void OnCellTap(int x, int y)
    {
        OnCellTapped?.Invoke(x, y);
    }

    private void AssignCellNeighbours()
    {
        foreach (CellView cellView in _cellViews)
        {
            if (cellView.X != Width - 1)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Right, _cellViews[cellView.X + 1, cellView.Y]);
            }
            if (cellView.X != 0)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Left, _cellViews[cellView.X - 1, cellView.Y]);
            }
            if (cellView.Y != Height - 1)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Up, _cellViews[cellView.X, cellView.Y + 1]);
            }
            if (cellView.Y != 0)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Down, _cellViews[cellView.X, cellView.Y - 1]);
            }
        }
    }

    public IEnumerator ExecuteCells(CellView tappedCell, List<CellView> cellsToExecute, ExecuteTypeEnum executeType, ItemTypeEnum itemType)
    {
        _isBussy = true;

        IExecutionStrategy executionStrategy = executeType switch
        {
            ExecuteTypeEnum.Blast => new BlastExecutionStrategy(),
            ExecuteTypeEnum.Merge => new MergeExecutionStrategy(itemFactory),
            ExecuteTypeEnum.Special => new SpecialExecutionStrategy(this),
            _ => throw new NotImplementedException()
        };

        yield return executionStrategy.Execute(tappedCell, cellsToExecute, itemType);

        _isBussy = false;

        fallManager.HandleBoardItems();
        fillManager.FillBoard();
    }

    /*
    public List<CellView> GetCellsInPerimeter(CellView centerCell, int perimeterRadius)
    {
        List<CellView> effectedCells = new();

        for (int y = -perimeterRadius; y <= perimeterRadius; y++)
        {
            var _y = centerCell.Y + y;
            if (_y < 0) continue;
            else if (_y > Height - 1) break;

            for (int x = -perimeterRadius; x <= perimeterRadius; x++)
            {
                var _x = centerCell.X + x;
                if (_x < 0) continue;
                else if (_x > Width - 1) break;

                effectedCells.Add(GetCellView(_x, _y));
            }
        }

        return effectedCells;
    }
    */

    public List<CellView> GetCellViews(Func<CellView, bool> condition)
    {
        List<CellView> matchingCells = new ();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                CellView cellView = _cellViews[x, y];
                if (condition(cellView))
                {
                    matchingCells.Add(cellView);
                }
            }
        }

        return matchingCells;
    }
}
