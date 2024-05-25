using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using UnityEngine;

public class CubeItemView : ItemView
{
    public override void Init(BoardView boardView, ExecutionManager executionManager, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.CubeItem;
        base.Init(boardView, executionManager, matchType);
    }

    public override void SetMatchableType(MatchTypeEnum matchType)
    {
        base.SetMatchableType(matchType);

        mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(Array.IndexOf(ItemDataParser.cubeItemTypes, MatchType));
    }

    public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType)
    {
        DestroyItem();
    }

    public override bool TryInteract(CellView currentCellView)
    {
        var cellsToExecute = MatchFinder.FindMatchCluster(currentCellView);
        if (cellsToExecute.Count < Config.BlastMinimumRequiredMatch) return false;

        var executionType = cellsToExecute.Count >= Config.TntMinimumRequiredMatch ? ExecuteTypeEnum.Merge : ExecuteTypeEnum.Blast;
        _executionManager.ExecuteCellViews(currentCellView, cellsToExecute, executionType);

        return true;
    }

    public override void Highight()
    {
        mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(Array.IndexOf(ItemDataParser.cubeItemTypes, MatchType) + Config.CubeTypeCount);
    }

    public override void Unhighight()
    {
        mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(Array.IndexOf(ItemDataParser.cubeItemTypes, MatchType));
    }
}
