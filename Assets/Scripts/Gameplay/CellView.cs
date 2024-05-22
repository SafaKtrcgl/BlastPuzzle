using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class CellView : MonoBehaviour, IPointerDownHandler
    {
        public Action<int, int> OnClickAction;

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
        }

        public void SetItemInside(ItemView item)
        {
            _itemInside = item;
        }

        public void Execute() 
        {
            _itemInside?.Execute();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClickAction?.Invoke(_x, _y);
        }

        public void AssignNeighbourCell(DirectionEnum neighbourCellDirection, CellView cellView)
        {
            _neighbours.Add(neighbourCellDirection, cellView);
        }
    }
}
