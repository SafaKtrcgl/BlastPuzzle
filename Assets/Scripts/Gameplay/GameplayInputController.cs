using Gameplay;
using System;
using UnityEngine;

public class GameplayInputController : MonoBehaviour
{
    public Action<int> OnTapPerform;

    private BoardView _boardView;
    
    private static int _moveCount;
    public static int MoveCount { get => _moveCount; }

    public void Init(BoardView boardView, int moveCount)
    {
        _boardView = boardView;
        _moveCount = moveCount;
    }

    public void OnCellTap(CellView clickedCellView)
    {
        if (_boardView.IsBussy) return;
        if (_moveCount < 1) return;

        var tappedCellItem = clickedCellView.ItemInside;
        if (tappedCellItem == null) return;

        if (!tappedCellItem.TryInteract(clickedCellView)) return;

        _moveCount--;
        OnTapPerform?.Invoke(_moveCount);
    }

    private void OnDestroy()
    {
        OnTapPerform = null;
    }
}
