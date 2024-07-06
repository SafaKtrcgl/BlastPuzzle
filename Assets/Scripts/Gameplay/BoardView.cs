using DG.Tweening;
using Enums;
using Extensions;
using Gameplay;
using Gameplay.Items;
using Gameplay.Managers;
using ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class BoardView : MonoBehaviour
{
    [SerializeField] private FillManager fillManager;
    [SerializeField] private FallManager fallManager;
    [SerializeField] private CellView cellViewPrefab;
    [SerializeField] private RectTransform boardBackgroundRecttransform;
    [SerializeField] private Transform cellViewHolderTransform;

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
                var cellView = GetOrCreateCellView();
                cellView.Init(this, x, y);
                _cellViews[x, y] = cellView;

                cellView.OnItemExecutedAction += _executionManager.OnItemExecuted;

                var index = y * Width + x;

                var itemType = ItemDataParser.GetItemType(content[index]);
                if (itemType == ItemTypeEnum.None) continue;
                
                var matchType = ItemDataParser.GetMatchType(content[index]);
                
                var itemView = _itemFactory.CreateItem(itemType, matchType);
                ((RectTransform)itemView.transform).anchoredPosition = ((RectTransform)cellView.transform).anchoredPosition;

                if (int.TryParse(new string(content[index].Where(char.IsDigit).ToArray()), out var state))
                {
                    itemView.SetState(state);
                }

                cellView.InsertItem(itemView);
            }
        }

        AssignCellNeighbours();

        boardBackgroundRecttransform.sizeDelta = new Vector2(Width * CellView.CellSize + BackgroundWidthPadding, Height * CellView.CellSize + BackgroundHeightPadding);

        Validate();
    }

    private CellView GetOrCreateCellView()
    {
        var cellView = PoolManager.Instance.GetFromPool<CellView>(RecyclableTypeEnum.Cell);
        if (cellView != null)
        {
            cellView.transform.SetParent(cellViewHolderTransform);
            return cellView;
        }

        return Instantiate(cellViewPrefab, cellViewHolderTransform);
    }

    private void AssignCellNeighbours()
    {
        foreach (CellView cellView in _cellViews)
        {
            if (cellView.X != Width - 1)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Right, GetCellView(cellView.X + 1, cellView.Y));
            }
            if (cellView.X != 0)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Left, GetCellView(cellView.X - 1, cellView.Y));
            }
            if (cellView.Y != Height - 1)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Up, GetCellView(cellView.X, cellView.Y + 1));
            }
            if (cellView.Y != 0)
            {
                cellView.AssignNeighbourCell(DirectionEnum.Down, GetCellView(cellView.X, cellView.Y - 1));
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
                CellView cellView = GetCellView(x, y);
                if (condition(cellView))
                {
                    matchingCells.Add(cellView);
                }
            }
        }

        return matchingCells;
    }

    public HashSet<CellView> GetCellViewsInPerimeter(CellView centerCell, int perimeter)
    {
        HashSet<CellView> surroundingCells = new();

        for (int y = -perimeter; y <= perimeter; y++)
        {
            var currentY = centerCell.Y + y;
            if (currentY < 0) continue;
            if (currentY >= Height) break;

            for (int x = -perimeter; x <= perimeter; x++)
            {
                var currentX = centerCell.X + x;
                if (currentX < 0) continue;
                if (currentX >= Width) break;

                surroundingCells.Add(GetCellView(currentX, currentY));
            }
        }

        return surroundingCells;
    }

    public void Validate()
    {
        var cubeItemCells = GetCellViews(cellView => cellView.ItemInside != null && cellView.ItemInside.ItemType == ItemTypeEnum.CubeItem).ToList();

        List<HashSet<CellView>> matchClusters = new();

        while (cubeItemCells.Count > 0)
        {
            var cubeItemCell = cubeItemCells[0];
            var matchCluster = MatchFinder.FindMatchCluster(cubeItemCell);

            matchClusters.Add(matchCluster);

            foreach (var clusterCell in matchCluster)
            {
                cubeItemCells.Remove(clusterCell);
            }
        }

        TryRecoverBoard(matchClusters);
        HighlightMatches(matchClusters);
        SaveCurrentProgress();
        IsBussy = false;
    }

    private void SaveCurrentProgress()
    {
        string[] currentGridData = new string[Width * Height];
        
        foreach (var cellView in GetCellViews(cellView => true))
        {
            var item = cellView.ItemInside;
            if (item == null)
            {
                currentGridData[Width * cellView.Y + cellView.X] = "";
            }
            else
            {
                currentGridData[Width * cellView.Y + cellView.X] = item.ToString();
            }
        }
        
        PlayerPrefsUtility.SetOnGoingLevelData(LevelDataParser.GetLevelJson(Width, Height, GameplayInputController.MoveCount, currentGridData));
    }

    private void TryRecoverBoard(List<HashSet<CellView>> matchClusters)
    {
        void CreateMiddleMatchCluster()
        {
            var middleCellView = GetCellView(Width / 2, Height / 2);
            ConvertItem(middleCellView, ItemTypeEnum.CubeItem, MatchTypeEnum.Blue);

            foreach (var middleCellNeighbour in middleCellView.Neighbours.Values)
            {
                ConvertItem(middleCellNeighbour, ItemTypeEnum.CubeItem, MatchTypeEnum.Blue);
            }
        }

        if (GetCellViews(cellView => cellView.ItemInside != null && cellView.ItemInside.ItemType.IsSpecial()).Count > 0) return;

        foreach (var matchCluster in matchClusters)
        {
            if (matchCluster.Count > Config.BlastMinimumRequiredMatch) return;
        }

        CreateMiddleMatchCluster();
    }

    public void ConvertItem(CellView cellView, ItemTypeEnum itemType, MatchTypeEnum matchType)
    {
        cellView.ItemInside?.DestroyItem(ExecuteTypeEnum.Blast);

        if (itemType == ItemTypeEnum.None) return;

        var itemView = _itemFactory.CreateItem(itemType, matchType);
        cellView.InsertItem(itemView);

        ((RectTransform)itemView.transform).anchoredPosition = ((RectTransform)cellView.transform).anchoredPosition;
    }

    private void HighlightMatches(List<HashSet<CellView>> matchClusters)
    {
        foreach (var matchCluster in matchClusters)
        {
            var isPotentialSpecialMatch = matchCluster.Count >= Config.TntMinimumRequiredMatch;

            foreach (var matchCell in matchCluster)
            {
                if (isPotentialSpecialMatch)
                {
                    matchCell.ItemInside.Highight();
                }
                else
                {
                    matchCell.ItemInside.Unhighight();
                }
            }
        }
    }

    public void OnGameEnded()
    {
        foreach (var cellView in GetCellViews(cellView => true))
        {
            PoolManager.Instance.SendToPool(cellView, RecyclableTypeEnum.Cell);
        }
    }
}
