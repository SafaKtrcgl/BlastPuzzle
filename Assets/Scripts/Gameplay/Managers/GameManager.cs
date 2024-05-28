using Enums;
using Extensions;
using Helpers;
using UI.Dialog;
using UnityEngine;
using Utilities;

namespace Gameplay.Managers
{
    public class GameManager : MonoBehaviour
    {
        private BoardView _boardView;
        public void Init(BoardView boardView)
        {
            _boardView = boardView;
        }

        public void CheckGameState()
        {
            if (_boardView.GetCellViews(cellView => cellView.ItemInside && cellView.ItemInside.ItemType.IsObstacle()).Count == 0)
            {
                PlayerPrefsUtility.SetCurrentLevel(PlayerPrefsUtility.GetCurrentLevel() + 1);
                PlayerPrefsUtility.SetOnGoingLevelData(null);

                var dialogHelper = HelperResources.Instance.GetHelper<DialogHelper>(HelperEnum.DialogHelper);
                dialogHelper.ShowDialog<LevelCompleteDialog>(DialogTypeEnum.LevelCompleteDialog).Init();
            }
            else if (GameplayInputController.MoveCount == 0)
            {
                PlayerPrefsUtility.SetOnGoingLevelData(null);

                var contextHelper = HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper);
                var dialogHelper = HelperResources.Instance.GetHelper<DialogHelper>(HelperEnum.DialogHelper);
                dialogHelper.ShowGenericPopupDialog("Level Failed!", "Try Again",
                    () => { contextHelper.LoadGameplayScene(); },
                    () => { contextHelper.LoadMainScene(); });
            }
            else
            {
                _boardView.Validate();
            }
        }
    }
}
