using DG.Tweening;
using Enums;
using Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private FillManager fillManager;
    [SerializeField] private FallManager fallManager;
    [SerializeField] private CellView cellViewPrefab;
    [SerializeField] private RectTransform boardBackgroundRecttransform;

    public Action<ItemTypeEnum> OnObstacleItemExecuted;

    private int _width;
    public int Width => _width;

    private int _height;
    public int Height => _height;

    private ItemFactory _itemFactory;

    private readonly float BackgroundWidthPadding = 35f;
    private readonly float BackgroundHeightPadding = 50f;

    private CellView[,] _cellViews;
    public CellView GetCellView(int x, int y) => _cellViews[x, y];

    private bool _isBussy = true;
    public bool IsBussy => _isBussy;

    public void Init(ItemFactory itemFactory, int width, int height, string[] content)
    {
        _width = width;
        _height = height;

        _cellViews = new CellView[_width, _height];

        _itemFactory = itemFactory;

        fallManager.Init(this);
        fillManager.Init(this, _itemFactory, fallManager);

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

                _cellViews[x, y] = cellView;
                
                var index = y * Width + x;

                var itemType = ItemDataParser.GetItemType(content[index]);
                var matchType = ItemDataParser.GetMatchType(content[index]);
                
                var itemView = _itemFactory.CreateItem(itemType, matchType);
                ((RectTransform)itemView.transform).anchoredPosition = ((RectTransform)cellView.transform).anchoredPosition;

                cellView.InsertItem(itemView);
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

    public void ExecuteCellViews(CellView originCellView, HashSet<CellView> cellViewsToExecute, ExecuteTypeEnum executeType)
    {
        StartCoroutine(ExecuteCellViewsCoroutine(originCellView, cellViewsToExecute, executeType));
    }

    private IEnumerator ExecuteCellViewsCoroutine(CellView originCellView, HashSet<CellView> cellViewsToExecute, ExecuteTypeEnum executeType)
    {
        _isBussy = true;

        IExecutionStrategy executionStrategy = executeType switch
        {
            ExecuteTypeEnum.Blast => new BlastExecutionStrategy(),
            ExecuteTypeEnum.Merge => new MergeExecutionStrategy(_itemFactory),
            ExecuteTypeEnum.Special => new SpecialExecutionStrategy(),
            ExecuteTypeEnum.Combo => new ComboExecutionStrategy(_itemFactory),
            _ => throw new NotImplementedException()
        };

        yield return StartCoroutine(executionStrategy.Execute(originCellView, cellViewsToExecute));

        _isBussy = false;

        fallManager.HandleBoardItems();
        fillManager.FillBoard();

        CheckIfGameEnded();
    }

    /*
    private ExecuteTypeEnum GetExecuteType(CellView originCellView, HashSet<CellView> cellViewsToExecute)
    {
        Debug.Log("Selamlar : > " + cellViewsToExecute.Count);

        if (originCellView.ItemInside.ItemType.IsSpecial())
        {
            return ExecuteTypeEnum.Special;
        }
        else
        {
            Debug.Log("Selamlar unspecial : > " + cellViewsToExecute.Count);
            if (cellViewsToExecute.Count >= 5)
                return ExecuteTypeEnum.Merge;
            else
                return ExecuteTypeEnum.Blast;
        }
    }
    */

    public HashSet<CellView> GetCellViews(Func<CellView, bool> condition)
    {
        HashSet<CellView> matchingCells = new ();

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

    private void CheckIfGameEnded()
    {
        /*
        if (GetCellViews(cellView => cellView.ItemInside.ItemType.IsObstacle()).Count == 0)
        {
            Debug.Log("Game Won!");
            //_isBussy = true;
        }
        else if (GameplayLogicController.MoveCount == 0)
        {
            Debug.Log("Game Lost!");
        }
        */
    }
}
