using UnityEngine;

public class FillManager : MonoBehaviour
{
    private BoardView _boardView;
    private ItemFactory _itemFactory;

    public void Init(BoardView boardView, ItemFactory itemFactory)
    {
        _boardView = boardView;
        _itemFactory = itemFactory;
    }

    /*
    public List<ItemView> FillBoard()
    {
        var emptyCells = _boardView.GetCellViews(cell => cell.ItemInside == null);
        
        
        foreach (var emptyCell in emptyCells)
        {
            FillColumn(emptyCellGroup.Key, emptyCellGroup.Count());
        }
    }
    */
}
