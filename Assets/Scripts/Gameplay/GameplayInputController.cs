using Gameplay;
using System;
using UnityEditor;
using UnityEngine;

public class GameplayInputController : MonoBehaviour
{
    public Action<int> OnTapPerform;
    public Action<int> OnAdminTouchPerform;

    private BoardView _boardView;
    private ItemFactory _itemFactory;
    
    private static int _moveCount;
    public static int MoveCount { get => _moveCount; }

    public void Init(BoardView boardView, ItemFactory itemFactory, int moveCount)
    {
        _boardView = boardView;
        _itemFactory = itemFactory;
        _moveCount = moveCount;
    }

    public void OnCellTap(CellView clickedCellView)
    {
        if (_boardView.IsBussy) return;
        if (_moveCount < 1) return;

#if UNITY_EDITOR
        EditorSettings settings = AssetDatabase.LoadAssetAtPath<EditorSettings>("Assets/Scripts/ScriptableObjects/EditorSettings/EditorSettings.asset");
        if (settings != null && settings.adminCreateItemTouch)
        {
            clickedCellView.ItemInside?.DestroyItem();

            var itemView = _itemFactory.CreateItem(settings.itemType, settings.matchType);
            clickedCellView.InsertItem(itemView);

            ((RectTransform)itemView.transform).anchoredPosition = ((RectTransform)clickedCellView.transform).anchoredPosition;
            itemView.transform.SetSiblingIndex(clickedCellView.transform.GetSiblingIndex());

            return;
        }
#endif

        var tappedCellItem = clickedCellView.ItemInside;
        if (tappedCellItem == null) return;

        if (!tappedCellItem.TryInteract(clickedCellView)) return;

        _moveCount--;
        OnTapPerform?.Invoke(_moveCount);
    }

    private void OnDestroy()
    {
        OnTapPerform = null;
    }
}
