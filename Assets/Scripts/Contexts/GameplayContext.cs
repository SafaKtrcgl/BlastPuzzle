using DG.Tweening;
using Helper;
using Singleton;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Context
{
    public class GameplayContext : ContextBase
    {
        [SerializeField] private BoardView boardView;
        [SerializeField] private Image arenaBackgroundImage;

        [SerializeField] private RectTransform topPanelRectTransform;

        private const float TopPanelDestinationPosY = 50f;
        private const float PanelAnimationDuration = .5f;

        private void Start()
        {
            var arenaResource = HelperResources.Instance.GetHelper<ArenaResourceHelper>(HelperEnum.ArenaResourceHelper).TryGetArenaResource(0);
            if (arenaResource != null)
            {
                arenaBackgroundImage.sprite = arenaResource.ArenaBackgroundSprite;
            }

            PlayInitializationAnimations();

            var levelData = LevelDataParser.GetLevelData(PlayerPrefsUtility.GetCurrentLevel());
            boardView.Init(levelData.grid_width, levelData.grid_height, levelData.grid);
        }

        private void PlayInitializationAnimations()
        {
            topPanelRectTransform.DOAnchorPosY(TopPanelDestinationPosY, PanelAnimationDuration).SetEase(Ease.OutBack);
        }
    }
}
