using Enums;
using Extensions;
using Interfaces.Strategy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Managers
{
    public class ExecutionManager : MonoBehaviour
    {
        public Action OnExecutionQueueEnd;
        public Action<ItemTypeEnum> OnObstacleItemExecuted;

        private BoardView _boardView;
        private ItemFactory _itemFactory;

        private Queue<ExecutionNode> _executionQueue = new();

        public void Init(BoardView boardView, ItemFactory itemFactory)
        {
            _boardView = boardView;
            _itemFactory = itemFactory;
        }

        public void ExecuteCellViews(CellView originCellView, HashSet<CellView> cellViewsToExecute, ExecuteTypeEnum executeType, int executionIndex)
        {
            if (_executionQueue.Count == 0)
            {
                _boardView.IsBussy = true;

                FillExecutionQueue(originCellView, cellViewsToExecute, executeType, 0);

                StartCoroutine(ExecuteExecutionQueue());
            }
            else
            {
                //Debug.Log("Selamlar :> " + executionIndex);
                FillExecutionQueue(originCellView, cellViewsToExecute, executeType, executionIndex);
            }
        }

        private void FillExecutionQueue(CellView originCellView, HashSet<CellView> cellViewsToExecute, ExecuteTypeEnum executeType, int executionIndex)
        {
            IExecutionStrategy executionStrategy = executeType switch
            {
                ExecuteTypeEnum.Blast => new BlastExecutionStrategy(),
                ExecuteTypeEnum.Merge => new MergeExecutionStrategy(_itemFactory),
                ExecuteTypeEnum.Special => new SpecialExecutionStrategy(),
                ExecuteTypeEnum.Combo => new ComboExecutionStrategy(_itemFactory),
                _ => throw new NotImplementedException()
            };

            var executionNode = new ExecutionNode(executionStrategy.Execute(originCellView, cellViewsToExecute, executionIndex), executionIndex);
            _executionQueue.Enqueue(executionNode);
        }

        private IEnumerator ExecuteExecutionQueue()
        {
            while (_executionQueue.Count > 0)
            {
                var currentExecutionIndex = _executionQueue.Peek().executionIndex;
                var executionNodesToExecute = _executionQueue.Where(x => x.executionIndex == currentExecutionIndex);

                HashSet<Coroutine> runningCoroutines = new ();

                foreach (var executionNode in executionNodesToExecute)
                {
                    Coroutine coroutine = StartCoroutine(executionNode.executionEnumerator);
                    runningCoroutines.Add(coroutine);
                }

                foreach (var coroutine in runningCoroutines)
                {
                    yield return coroutine;
                }

                _executionQueue = new Queue<ExecutionNode>(_executionQueue.Where(x => x.executionIndex != currentExecutionIndex));
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
}
