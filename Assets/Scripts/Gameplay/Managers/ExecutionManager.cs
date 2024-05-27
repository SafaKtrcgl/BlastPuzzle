using DG.Tweening;
using Enums;
using Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutionManager : MonoBehaviour
{
    public Action OnExecutionQueueEnd;
    public Action<ItemTypeEnum> OnObstacleItemExecuted;

    private BoardView _boardView;
    private ItemFactory _itemFactory;

    private Queue<IEnumerator> _executionQueue = new();

    public void Init(BoardView boardView, ItemFactory itemFactory)
    {
        _boardView = boardView;
        _itemFactory = itemFactory;
    }

    public void ExecuteCellViews(CellView originCellView, HashSet<CellView> cellViewsToExecute, ExecuteTypeEnum executeType)
    {
        if (_executionQueue.Count == 0)
        {
            _boardView.IsBussy = true;

            ConstructExecutionQueue(originCellView, cellViewsToExecute, executeType);

            StartCoroutine(ExecuteExecutionQueue());
        }
        else
        {
            ConstructExecutionQueue(originCellView, cellViewsToExecute, executeType);
        }
    }

    private void ConstructExecutionQueue(CellView originCellView, HashSet<CellView> cellViewsToExecute, ExecuteTypeEnum executeType)
    {
        IExecutionStrategy executionStrategy = executeType switch
        {
            ExecuteTypeEnum.Blast => new BlastExecutionStrategy(),
            ExecuteTypeEnum.Merge => new MergeExecutionStrategy(_itemFactory),
            ExecuteTypeEnum.Special => new SpecialExecutionStrategy(),
            ExecuteTypeEnum.Combo => new ComboExecutionStrategy(_itemFactory),
            _ => throw new NotImplementedException()
        };

        _executionQueue.Enqueue(executionStrategy.Execute(originCellView, cellViewsToExecute));

        if (executeType == ExecuteTypeEnum.Special)
        {
            foreach (var cellViewToExecute in cellViewsToExecute)
            {
                if (!cellViewToExecute.ItemInside) continue;
                if (!cellViewToExecute.ItemInside.ItemType.IsSpecial()) continue;

                cellViewToExecute.Execute(executeType);
            }
        }
    }

    private IEnumerator ExecuteExecutionQueue()
    {
        while (_executionQueue.Count > 0)
        {
            yield return _executionQueue.Peek();
            _executionQueue.Dequeue();
        }

        yield return new WaitForEndOfFrame();

        OnExecutionQueueEnd?.Invoke();
    }

    public void OnItemExecuted(ItemTypeEnum itemType)
    {
        if (itemType.IsObstacle())
        {
            OnObstacleItemExecuted?.Invoke(itemType);
        }
    }

    private void OnDestroy()
    {
        OnObstacleItemExecuted = null;
        OnExecutionQueueEnd = null;
    }
}
