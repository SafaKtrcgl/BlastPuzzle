using UnityEngine;

public class InputListener : MonoBehaviour
{
    private BoardView _boardView;
    public void Init(BoardView boardView)
    {
        _boardView = boardView;
    }

    public void OnCellTap(int x, int y)
    {
        if (_boardView.IsBussy) return;

        var cellView = _boardView.GetCellView(x, y);
        if (cellView.ItemInside != null && cellView.ItemInside.IsMatchable)
        {
            var matchingCells = MatchFinder.FindMatchCluster(cellView);
            
            if (matchingCells != null && matchingCells.Count < 2) return;

            _boardView.ExecuteCells(matchingCells);
        }
    }
}
