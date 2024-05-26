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
        [SerializeField] private ExecutionManager executionManager;
        [SerializeField] private GameManager gameManager;

        [SerializeField] private Image arenaBackgroundImage;

        private void Start()
        {
            var arenaResource = HelperResources.Instance.GetHelper<ArenaResourceHelper>(HelperEnum.ArenaResourceHelper).TryGetArenaResource(0);
            if (arenaResource != null)
            {
                arenaBackgroundImage.sprite = arenaResource.ArenaBackgroundSprite;
            }

            LevelDataParser levelData;

            if (string.IsNullOrEmpty(PlayerPrefsUtility.GetOnGoingLevelData()))
            {
                levelData = LevelDataParser.GetLevelData(PlayerPrefsUtility.GetCurrentLevel());
            }
            else
            {
                levelData = LevelDataParser.GetLevelData(PlayerPrefsUtility.GetOnGoingLevelData());
            }

            inputController.OnTapPerform += gameplayTopPanel.OnMovePerformed;

            executionManager.OnExecutionQueueEnd += fallManager.HandleBoardItems;
            fallManager.OnBoardItemFallEnd += fillManager.FillBoard;
            fillManager.OnBoardFilled += gameManager.CheckGameState;

            itemFactory.OnObstacleItemCreated += gameplayTopPanel.OnObstacleCreated;
            executionManager.OnObstacleItemExecuted += gameplayTopPanel.OnObstacleExecuted;

            gameManager.Init(boardView);
            executionManager.Init(boardView, itemFactory);
            itemFactory.Init(boardView, executionManager);
            boardView.Init(itemFactory, executionManager, levelData.grid_width, levelData.grid_height, levelData.grid);
            fallManager.Init(boardView);
            fillManager.Init(boardView, itemFactory, fallManager);

            foreach (var cellView in boardView.GetCellViews(x => true))
            {
                cellView.OnCellClicked += inputController.OnCellTap;
            }
            
            inputController.Init(boardView, itemFactory, levelData.move_count);
            gameplayTopPanel.Init(boardView, levelData.move_count);
        }
    }
}
