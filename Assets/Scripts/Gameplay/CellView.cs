using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Gameplay
{
    public class CellView : MonoBehaviour
    {
        private int _x;
        private int _y;

        private Dictionary<DirectionEnum, CellView> _neighbours = new();
        public Dictionary<DirectionEnum, CellView> Neighbours => _neighbours;

        private Item _itemInside;
        public Item ItemInside { get => _itemInside; }

        public void Init(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void SetItemInside(Item item)
        {
            _itemInside = item;
        }
    }
}
