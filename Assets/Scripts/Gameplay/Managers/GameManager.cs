using Enums;
using Extensions;
using Helpers;
using System;
using UI.Dialog;
using UnityEngine;
using Utilities;

namespace Gameplay.Managers
{
    public class GameManager : MonoBehaviour
    {
        public Action OnGameEnded;
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
                dialogHelper.ShowDialog<LevelCompleteDialog>(DialogTypeEnum.LevelCompleteDialog).Init(() => OnGameEnded?.Invoke());
            }
            else if (GameplayInputController.MoveCount == 0)
            {
                PlayerPrefsUtility.SetOnGoingLevelData(null);

                var contextHelper = HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper);
                var dialogHelper = HelperResources.Instance.GetHelper<DialogHelper>(HelperEnum.DialogHelper);
                dialogHelper.ShowGenericPopupDialog("Level Failed!\n\nSeems like you are out of moves.", "Try Again",
                    () => 
                    { 
                        contextHelper.LoadGameplayScene();
                        OnGameEnded?.Invoke();
                    },
                    () =>
                    { 
                        contextHelper.LoadMainScene();
                        OnGameEnded?.Invoke();
                    });
            }
            else
            {
                _boardView.Validate();
            }
        }
    }
}
