using DG.Tweening;
using Enums;
using Gameplay;
using Helper;
using Singleton;
using System;
using UnityEngine;

public class CubeItemView : ItemView
{
    public override void Init(BoardView boardView, ExecutionManager executionManager, PoolManager poolManager, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.CubeItem;
        base.Init(boardView, executionManager, poolManager, matchType);
    }

    public override void SetMatchableType(MatchTypeEnum matchType)
    {
        base.SetMatchableType(matchType);

        var index = Array.IndexOf(ItemDataParser.cubeItemTypes, MatchType);

        mainSprite.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(index);

        ((RectTransform)mainSprite.transform).sizeDelta /= 2f;

        var sheetAnimation = destroyParticleSystem.textureSheetAnimation;
        sheetAnimation.SetSprite(0, sheetAnimation.GetSprite(index));
    }

    public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType)
    {
        DestroyItem(executeType);
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
        mainSprite.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(Array.IndexOf(ItemDataParser.cubeItemTypes, MatchType) + Config.CubeTypeCount);
    }

    public override void Unhighight()
    {
        mainSprite.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(Array.IndexOf(ItemDataParser.cubeItemTypes, MatchType));
    }

    public override void Recycle()
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;
        destroyParticleSystem.gameObject.SetActive(false);
        mainSprite.enabled = true;
        SetSpriteSortingLayer("Item");
        
        _poolManager.SendToPool(this, ItemType);
    }

    public override void DestroyItem(ExecuteTypeEnum executeType)
    {
        OnItemExecute?.Invoke(ItemType);
        mainSprite.enabled = false;
        
        if (executeType == ExecuteTypeEnum.Merge)
        {
            OnDestroyParticleEnd();
        }
        else
        {
            PlayDestroyParticles();
        }
    }
}
