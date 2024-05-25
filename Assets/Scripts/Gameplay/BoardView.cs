using DG.Tweening;
using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

public class BoardView : MonoBehaviour
{
    [SerializeField] private FillManager fillManager;
    [SerializeField] private FallManager fallManager;
    [SerializeField] private CellView cellViewPrefab;
    [SerializeField] private RectTransform boardBackgroundRecttransform;
    //[SerializeField] private CoroutineQueue coroutineQueue;

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
    public bool IsBussy { get => _isBussy; set => _isBussy = value; }

    public void Init(ItemFactory itemFactory, int width, int height, string[] content)
    {
        _width = width;
        _height = height;

        _cellViews = new CellView[_width, _height];

        _itemFactory = itemFactory;

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

                cellView.OnItemExecutedAction += OnItemExecuted;

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

    public void OnItemExecuted(ItemTypeEnum itemType)
    {
        if (itemType.IsObstacle())
        {
            OnObstacleItemExecuted?.Invoke(itemType);
        }
    }

    public void OnBoardFilled()
    {
        _isBussy = false;
    }

    private void CheckIfGameEnded()
    {
        if (GetCellViews(cellView => cellView.ItemInside && cellView.ItemInside.ItemType.IsObstacle()).Count == 0)
        {
            _isBussy = true;
            PlayerPrefsUtility.SetCurrentLevel(PlayerPrefsUtility.GetCurrentLevel() + 1);
            var dialogHelper = HelperResources.Instance.GetHelper<DialogHelper>(HelperEnum.DialogHelper);
            dialogHelper.ShowDialog<LevelCompleteDialog>(DialogTypeEnum.LevelCompleteDialog).Init();
        }
        else if (GameplayInputController.MoveCount == 0)
        {
            _isBussy = true;
            var contextHelper = HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper);
            var dialogHelper = HelperResources.Instance.GetHelper<DialogHelper>(HelperEnum.DialogHelper);
            dialogHelper.ShowGenericPopupDialog("Level Failed!", "Try Again",
                () => { contextHelper.LoadGameplayScene(); }, 
                () => { contextHelper.LoadMainScene(); });
        }
    }

    private void OnDestroy()
    {
        OnObstacleItemExecuted = null;
    }
}
