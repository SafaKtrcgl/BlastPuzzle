using Enums;
using Gameplay;
using Helper;
using Singleton;
using UnityEngine;

public class VaseItemView : ItemView
{
    private float _currentHp = 2;
    int _lastAffectedExecutionId = -1;

    public override void Init(BoardView boardView, ExecutionManager executionManager, PoolManager poolManager, MatchTypeEnum matchType)
    {
        ItemType = ItemTypeEnum.VaseItem;
        base.Init(boardView, executionManager, poolManager, matchType);
    }

    public override void Execute(int executionId, CellView currentCellView, ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Special)
        {
            TakeHit(executionId, executeType);
        }
    }

    public override void OnNeighbourExecute(int executionId, ExecuteTypeEnum executeType)
    {
        if (executeType == ExecuteTypeEnum.Blast || executeType == ExecuteTypeEnum.Merge)
        {
            if (_lastAffectedExecutionId == executionId) return;
            _lastAffectedExecutionId = executionId;

            TakeHit(executionId, executeType);
        }
    }

    private void TakeHit(int executionId, ExecuteTypeEnum executeType)
    {
        switch (_currentHp)
        {
            case 2:
                SetState("1");
                break;

            case 1:
                DestroyItem(executeType);
                break;
        }
    }

    public override void SetState(string currentState)
    {
        State = currentState;
        if (State == "1")
        {
            SetStateBroken();
        }
    }

    private void SetStateBroken()
    {
        mainSprite.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(ItemType).ItemSprite(1);

        _currentHp = 1;
    }
}
