using Enums;
using Gameplay;
using Helper;
using Singleton;
using System.Collections.Generic;

public class VaseItemView : ItemView
{
    private float _currentHp = 2;
    int _lastAffectedExecutionId = -1;

    public override void Init(BoardView boardView, ExecutionManager executionManager, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.VaseItem;
        base.Init(boardView, executionManager, matchType);
    }
    public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            TakeHit(executionId);
        }
    }

    public override void OnNeighbourExecute(int executionId, ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Blast || executeType == ExecuteTypeEnum.Merge)
        {
            if (_lastAffectedExecutionId == executionId) return;
            _lastAffectedExecutionId = executionId;

            TakeHit(executionId);
        }
    }

    private void TakeHit(int executionId)
    {
        _currentHp--;
        switch (_currentHp)
        {
            case 1:
                mainImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(1);
                break;

            case 0:
                DestroyItem();
                break;
        }
    }
}
