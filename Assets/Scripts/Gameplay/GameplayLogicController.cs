using Enums;
using UnityEngine;

public class GameplayLogicController : MonoBehaviour
{
    private BoardView _boardView;

    private readonly int minimumRequiredMatch = 2;


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
            
            if (matchingCells != null && matchingCells.Count < minimumRequiredMatch) return;

            _boardView.ExecuteCells(matchingCells, ExecuteTypeEnum.Blast);
        }
    }
}
