using DG.Tweening;
using Enums;
using System;
using UnityEngine;

public class GameplayLogicController : MonoBehaviour
{
    public Action<int> OnTapPerform;

    private BoardView _boardView;
    
    private static int _moveCount;
    public static int MoveCount { get => _moveCount; }

    private readonly int blastMinimumRequiredMatch = 2;
    private readonly int tntMinimumRequiredMatch = 5;

    public void Init(BoardView boardView, int moveCount)
    {
        _boardView = boardView;
        _moveCount = moveCount;
    }

    public void OnCellTap(int x, int y)
    {
        if (_boardView.IsBussy) return;
        if (_moveCount < 1) return;

        var tappedCell = _boardView.GetCellView(x, y);
        var tappedCellItem = tappedCell.ItemInside;
        if (tappedCellItem == null) return;

        _moveCount--;

        if (tappedCellItem.IsSpecial)
        {
            StartCoroutine(_boardView.ExecuteCells(tappedCell, null, ExecuteTypeEnum.Special, ItemTypeEnum.TntItem));
        }
        else if (tappedCell.ItemInside != null && tappedCell.ItemInside.IsMatchable)
        {
            var matchingCells = MatchFinder.FindMatchCluster(tappedCell);

            if (matchingCells.Count < blastMinimumRequiredMatch)
            {
                tappedCell.ItemInside.transform.DOPunchRotation(Vector3.forward * 25, .25f, 25, .75f);
                return;
            }
            else if (matchingCells.Count >= tntMinimumRequiredMatch)
            {
                StartCoroutine(_boardView.ExecuteCells(tappedCell, matchingCells, ExecuteTypeEnum.Merge, ItemTypeEnum.TntItem));
            }
            else
            {
                StartCoroutine(_boardView.ExecuteCells(tappedCell, matchingCells, ExecuteTypeEnum.Blast, ItemTypeEnum.None));
            }
        }

        OnTapPerform?.Invoke(_moveCount);
    }
}
