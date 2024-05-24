using DG.Tweening;
using Gameplay;
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
        [SerializeField] private GameplayTopPanel gameplayTopPanel;
        [SerializeField] private GameplayInputController inputController;
        [SerializeField] private ItemFactory itemFactory;
        [SerializeField] private FallManager fallManager;
        [SerializeField] private FillManager fillManager;

        [SerializeField] private Image arenaBackgroundImage;

        private void Start()
        {
            var arenaResource = HelperResources.Instance.GetHelper<ArenaResourceHelper>(HelperEnum.ArenaResourceHelper).TryGetArenaResource(0);
            if (arenaResource != null)
            {
                arenaBackgroundImage.sprite = arenaResource.ArenaBackgroundSprite;
            }

            var levelData = LevelDataParser.GetLevelData(PlayerPrefsUtility.GetCurrentLevel());

            inputController.OnTapPerform += gameplayTopPanel.OnMovePerformed;

            boardView.OnCellViewExecutionEnd += fallManager.HandleBoardItems;
            fallManager.OnBoardItemFallEnd += fillManager.FillBoard;
            fillManager.OnBoardFilled += boardView.OnBoardFilled;

            itemFactory.OnObstacleItemCreated += gameplayTopPanel.OnObstacleCreated;
            boardView.OnObstacleItemExecuted += gameplayTopPanel.OnObstacleExecuted;

            itemFactory.Init(boardView);
            boardView.Init(itemFactory, levelData.grid_width, levelData.grid_height, levelData.grid);
            fallManager.Init(boardView);
            fillManager.Init(boardView, itemFactory, fallManager);

            foreach (var cellView in boardView.GetCellViews(x => true))
            {
                cellView.OnCellClicked += inputController.OnCellTap;
            }
            
            inputController.Init(boardView, levelData.move_count);
            gameplayTopPanel.Init(boardView, levelData.move_count);
        }
    }
}
