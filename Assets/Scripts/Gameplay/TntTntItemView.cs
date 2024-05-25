using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using UnityEngine;

public class TntTntItemView : ComboItemView
{
    private int _perimeter = 3;
    public override void Init(BoardView boardView, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.TntTntItem;
        _boardView = boardView;
        MatchType = matchType;
        mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemTypeEnum.TntItem).ItemSprite(0);
    }

    public override void Execute(CellView currentCellView, ExecuteTypeEnum executeType)
    {
        if (IsDestinedToDie) return;
        IsDestinedToDie = true;

        var cellsToExecute = _boardView.GetCellViews(cellView =>
        {
            int deltaX = Math.Abs(cellView.X - currentCellView.X);
            int deltaY = Math.Abs(cellView.Y - currentCellView.Y);
            return deltaX <= _perimeter && deltaY <= _perimeter && !(deltaX == 0 && deltaY == 0);
        });

        _boardView.ExecuteCellViews(currentCellView, cellsToExecute, executeType);
    }
}
