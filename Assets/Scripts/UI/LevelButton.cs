using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button levelButton;
        [SerializeField] private TextMeshProUGUI levelButtonText;

        public void Init()
        {
            var areLevelFinished = PlayerPrefsUtility.GetCurrentLevel() > Config.LevelCount;

            levelButton.interactable = !areLevelFinished;
            levelButtonText.text = areLevelFinished ? "Finished" : $"Level {PlayerPrefsUtility.GetCurrentLevel()}";
        }
    }
}
