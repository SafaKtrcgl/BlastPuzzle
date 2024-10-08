using Enums;
using Gameplay.Items;
using Gameplay.Managers;
using Interfaces.Recycle;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class CellView : MonoBehaviour, IPointerDownHandler, IRecyclable
    {
        public Action<int, ExecuteTypeEnum> OnCellExecuteAction;
        public Action<ItemTypeEnum> OnItemExecutedAction;
        public Action<CellView> OnCellClicked;

        public const int CellSize = 70;

        private BoardView _boardView;

        private int _x;
        public int X => _x;
        private int _y;
        public int Y => _y;

        private Dictionary<DirectionEnum, CellView> _neighbours = new();
        public Dictionary<DirectionEnum, CellView> Neighbours => _neighbours;

        private ItemView _itemInside;
        public ItemView ItemInside { get => _itemInside; }

        public GameObject RecyclableGameObject { get; set; }

        public RecyclableTypeEnum RecyclableType { get; set; }

        public void Init(BoardView boardView, int x, int y)
        {
            _x = x;
            _y = y;
            _boardView = boardView;

            RecyclableGameObject = gameObject;
            RecyclableType = RecyclableTypeEnum.Cell;

            ((RectTransform)transform).anchoredPosition = new Vector2((x - ((_boardView.Width - 1) / 2f)) * CellSize, (y - ((_boardView.Height - 1) / 2f)) * CellSize);
        }

        public void InsertItem(ItemView item)
        {
            _itemInside = item;
            _itemInside.OnItemExecute += OnItemExecuted;
            item.SetSpriteSortingOrder(Y);
        }

        public ItemView ExtractItem()
        {
            var itemView = _itemInside;
            _itemInside.OnItemExecute -= OnItemExecuted;
            _itemInside = null;
            return itemView;
        }

        public void Execute(ExecuteTypeEnum executeType, int executionIndex)
        {
            if (ItemInside != null)
            {
                var executionId = GameplayInputController.MoveCount;
                OnCellExecuteAction?.Invoke(executionId, executeType);
                ItemInside?.Execute(executionId, this, executeType, executionIndex);
            }
        }

        public void OnNeighbourExecute(int executionId, ExecuteTypeEnum executeType)
        {
            _itemInside?.OnNeighbourExecute(executionId, executeType);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnCellClicked?.Invoke(this);
        }

        public void AssignNeighbourCell(DirectionEnum neighbourCellDirection, CellView cellView)
        {
            _neighbours.Add(neighbourCellDirection, cellView);
            cellView.OnCellExecuteAction += OnNeighbourExecute;
        }

        public void OnItemExecuted(ItemTypeEnum itemType)
        {
            OnItemExecutedAction?.Invoke(itemType);
            _itemInside = null;
        }

        private void OnDestroy()
        {
            OnCellExecuteAction = null;
            OnItemExecutedAction = null;
            OnCellClicked = null;
        }

        public void Recycle()
        {
            _neighbours.Clear();
            ExtractItem();
            OnCellExecuteAction = null;
            OnItemExecutedAction = null;
            OnCellClicked = null;
        }
    }
}
