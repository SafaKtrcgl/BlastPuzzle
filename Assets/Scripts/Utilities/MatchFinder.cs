using Enums;
using Gameplay;
using System.Collections.Generic;

namespace Utilities
{
    public static class MatchFinder
    {
        public static HashSet<CellView> FindMatchCluster(CellView tappedCell)
        {
            if (tappedCell.ItemInside == null || tappedCell.ItemInside.MatchType == MatchTypeEnum.None) return null;

            HashSet<CellView> matchingCells = new HashSet<CellView>();

            FindMatchCluster(tappedCell, tappedCell.ItemInside.MatchType, matchingCells);

            return matchingCells;
        }

        private static void FindMatchCluster(CellView cellView, MatchTypeEnum matchType, HashSet<CellView> matchingCells)
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
}
