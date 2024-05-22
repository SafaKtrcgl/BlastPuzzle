using Enums;
using Gameplay;
using System.Collections.Generic;

public static class MatchFinder
{
    public static List<CellView> FindMatchCluster(CellView tappedCell)
    {
        if (tappedCell.ItemInside == null || tappedCell.ItemInside.MatchType == MatchTypeEnum.None) return null;

        List<CellView> matchingCells = new List<CellView>();

        FindMatchCluster(tappedCell, tappedCell.ItemInside.MatchType, matchingCells);

        return matchingCells;
    }

    private static void FindMatchCluster(CellView cellView, MatchTypeEnum matchType, List<CellView> matchingCells)
    {
        

        if (matchingCells.Contains(cellView))
        {
            return;
        }

        matchingCells.Add(cellView);

        foreach (CellView neighbourCell in cellView.Neighbours.Values)
        {
            if (neighbourCell.ItemInside != null && neighbourCell.ItemInside.MatchType == matchType)
            {
                FindMatchCluster(neighbourCell, matchType, matchingCells);
            }
        }
    }
}
