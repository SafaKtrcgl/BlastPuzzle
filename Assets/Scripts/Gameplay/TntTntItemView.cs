using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using UnityEngine;

public class TntTntItemView : ComboItemView
{
    public override void Init(BoardView boardView, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.TntTntItem;
        _boardView = boardView;
        MatchType = matchType;
        mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemTypeEnum.TntItem).ItemSprite(0);
    }

    public override void Execute(CellView currentCellView, ExecuteTypeEnum executeType)
    {
        var cellsToExecute = _boardView.GetCellViews(cellView =>
        {
            int deltaX = Math.Abs(cellView.X - currentCellView.X);
            int deltaY = Math.Abs(cellView.Y - currentCellView.Y);
            return deltaX <= 3 && deltaY <= 3 && !(deltaX == 0 && deltaY == 0);
        });

        IsDestinedToDie = true;

        _boardView.ExecuteCellViews(currentCellView, cellsToExecute, executeType);

        DestroyItem();
    }
}
