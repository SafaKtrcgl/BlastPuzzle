using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using UnityEngine;

public class CubeItemView : ItemView
{
    public override void Init(BoardView boardView, MatchTypeEnum matchType)
    {
        _boardView = boardView;
        ItemType = ItemTypeEnum.CubeItem;
        MatchType = matchType;

        mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(Array.IndexOf(ItemDataParser.cubeItemTypes, MatchType));
    }

    public override void Execute(CellView currentCellView, ExecuteTypeEnum executeType)
    {
        DestroyItem();
    }

    public override bool TryInteract(CellView currentCellView)
    {
        var cellsToExecute = MatchFinder.FindMatchCluster(currentCellView);
        if (cellsToExecute.Count < Config.BlastMinimumRequiredMatch) return false;

        var executionType = cellsToExecute.Count >= Config.TntMinimumRequiredMatch ? ExecuteTypeEnum.Merge : ExecuteTypeEnum.Blast;
        _boardView.ExecuteCellViews(currentCellView, cellsToExecute, executionType);

        return true;
    }
}
