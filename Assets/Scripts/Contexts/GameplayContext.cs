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
        [SerializeField] private GameplayTopPanel gameplayTopPanel;
        [SerializeField] private GameplayLogicController logicController;
        [SerializeField] private ItemFactory itemFactory;

        [SerializeField] private Image arenaBackgroundImage;

        private void Start()
        {
            var arenaResource = HelperResources.Instance.GetHelper<ArenaResourceHelper>(HelperEnum.ArenaResourceHelper).TryGetArenaResource(0);
            if (arenaResource != null)
            {
                arenaBackgroundImage.sprite = arenaResource.ArenaBackgroundSprite;
            }

            var levelData = LevelDataParser.GetLevelData(PlayerPrefsUtility.GetCurrentLevel());

            boardView.OnCellTapped += logicController.OnCellTap;
            logicController.OnTapPerform += gameplayTopPanel.OnMovePerformed;
            itemFactory.OnObstacleItemCreated += gameplayTopPanel.OnObstacleCreated;
            boardView.OnObstacleItemExecuted += gameplayTopPanel.OnObstacleExecuted;

            boardView.Init(itemFactory, levelData.grid_width, levelData.grid_height, levelData.grid);
            logicController.Init(boardView, levelData.move_count);
            gameplayTopPanel.Init(boardView, levelData.move_count);
        }        
    }
}
