using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameplayTopPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moveCountText;

    private BoardView _boardView;
    private int _moveCount;

    public void Init(BoardView boardView, int moveCount)
    {
        _boardView = boardView;
        _moveCount = moveCount;

        ((RectTransform)transform).DOAnchorPosY(50, .5f).SetEase(Ease.OutBack);

        moveCountText.text = moveCount.ToString();
    }

    public void OnMovePerformed(int moveCount)
    {
        moveCountText.text = moveCount.ToString();
    }

    public void OnObstacleRemoved()
    {

    }
}
