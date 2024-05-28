using DG.Tweening;
using Enums;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class GameplayTopPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moveCountText;
        [SerializeField] private MissionView missionViewPrefab;
        [SerializeField] private Transform missionViewHolder;

        private BoardView _boardView;
        private int _moveCount;

        private Dictionary<ItemTypeEnum, MissionView> itemTypeMissionDictionary = new();

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

        public void OnObstacleCreated(ItemTypeEnum itemType)
        {
            if (itemTypeMissionDictionary.ContainsKey(itemType))
            {
                itemTypeMissionDictionary[itemType].UpdateCount(1);
            }
            else
            {
                var missionView = Instantiate(missionViewPrefab, missionViewHolder);
                missionView.Init(itemType);
                itemTypeMissionDictionary.Add(itemType, missionView);
            }
        }

        public void OnObstacleExecuted(ItemTypeEnum itemType)
        {
            itemTypeMissionDictionary[itemType].UpdateCount(-1);

            if (itemTypeMissionDictionary[itemType].IsComplete)
            {
                itemTypeMissionDictionary.Remove(itemType);
            }
        }
    }
}
