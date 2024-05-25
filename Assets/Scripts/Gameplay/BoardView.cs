using DG.Tweening;
using Enums;
using Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private FillManager fillManager;
    [SerializeField] private FallManager fallManager;
    [SerializeField] private CellView cellViewPrefab;
    [SerializeField] private RectTransform boardBackgroundRecttransform;

    private int _width;
    public int Width => _width;

    private int _height;
    public int Height => _height;

    private ItemFactory _itemFactory;
    private ExecutionManager _executionManager;


    private readonly float BackgroundWidthPadding = 35f;
    private readonly float BackgroundHeightPadding = 50f;

    private CellView[,] _cellViews;
    public CellView GetCellView(int x, int y) => _cellViews[x, y];

    private bool _isBussy = true;
    public bool IsBussy { get => _isBussy; set => _isBussy = value; }

    public void Init(ItemFactory itemFactory, ExecutionManager executionManager, int width, int height, string[] content)
    {
        _width = width;
        _height = height;

        _cellViews = new CellView[_width, _height];

        _itemFactory = itemFactory;
        _executionManager = executionManager;

        ConstructBoard(content);

        ((RectTransform)transform).DOAnchorPosX(0, .5f).SetEase(Ease.OutBack).OnComplete(() => IsBussy = false);
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

                cellView.OnItemExecutedAction += _executionManager.OnItemExecuted;

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

        HighlightMatches();
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

    public void Validate()
    {
        HighlightMatches();
        IsBussy = false;
    }
    /*
    private void TryShuffleBoard()
    {
        if (GetCellViews(cellView => cellView.ItemInside != null && cellView.ItemInside.ItemType.IsSpecial()).Count > 0) return;

        var cubeItemCells = GetCellViews(cellView => cellView.ItemInside != null && cellView.ItemInside.ItemType == ItemTypeEnum.CubeItem);
        HashSet<CellView> visitedCells = new ();

        foreach (var cubeItemCell in cubeItemCells)
        {
            if (visitedCells.Contains(cubeItemCell)) continue;
            var matchCluster = MatchFinder.FindMatchCluster(cubeItemCell);
            if (matchCluster.Count > Config.BlastMinimumRequiredMatch) return;

            foreach (var clusterCell in MatchFinder.FindMatchCluster(cubeItemCell))
            {
                visitedCells.Add(clusterCell);
            }
        }

        ShuffleBoard(cubeItemCells);
    }

    private void ShuffleBoard(HashSet<CellView> shuffleCells)
    {
        Sequence shuffleSequence = DOTween.Sequence();


    }
    */

    private void HighlightMatches()
    {
        var cubeItemCells = GetCellViews(cellView => cellView.ItemInside != null && cellView.ItemInside.ItemType == ItemTypeEnum.CubeItem);
        Debug.Log("Selamlar :> " + cubeItemCells.Count);

        HashSet<CellView> visitedCells = new();

        foreach (var cubeItemCell in cubeItemCells)
        {
            if (visitedCells.Contains(cubeItemCell)) continue;

            var matchCluster = MatchFinder.FindMatchCluster(cubeItemCell);
            var isPotentialSpecialMatch = matchCluster.Count >= Config.TntMinimumRequiredMatch;

            foreach (var matchCell in matchCluster)
            {
                visitedCells.Add(matchCell);

                if (isPotentialSpecialMatch)
                {
                    Debug.Log("Potential Match found");
                    matchCell.ItemInside.Highight();
                }
                else
                {
                    Debug.Log("No Potential Match found");
                    matchCell.ItemInside.Unhighight();
                }
            }
        }
    }
}
