using Enums;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class CellView : MonoBehaviour, IPointerDownHandler
    {
        public Action<ExecuteTypeEnum> OnCellExecuteAction;
        public Action<ItemTypeEnum> OnItemExecutedAction;

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

        public void Init(BoardView boardView, int x, int y)
        {
            _x = x;
            _y = y;
            _boardView = boardView;

            ((RectTransform)transform).anchoredPosition = new Vector2((x - ((_boardView.Width - 1) / 2f)) * CellSize, (y - ((_boardView.Height - 1) / 2f)) * CellSize);
        }

        public void InsertItem(ItemView item)
        {
            _itemInside = item;
            _itemInside.OnItemExecute += OnItemExecuted;
        }

        public ItemView ExtractItem()
        {
            var itemView = _itemInside;
            _itemInside.OnItemExecute -= OnItemExecuted;
            _itemInside = null;
            return itemView;
        }

        public void Execute(ExecuteTypeEnum executeType)
        {
            if (ItemInside != null && !ItemInside.IsDestinedToDie)
            {
                OnCellExecuteAction?.Invoke(executeType);
                ItemInside?.Execute(this, executeType);
            }
        }

        public void OnNeighbourExecute(ExecuteTypeEnum executeType)
        {
            _itemInside?.OnNeighbourExecute(executeType);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_boardView.IsBussy) return;

            ItemInside.Interact(this);
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
    }
}
