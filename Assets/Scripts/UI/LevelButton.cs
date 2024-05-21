using Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button levelButton;
        [SerializeField] private TextMeshProUGUI levelButtonText;

        public void Init()
        {
            levelButtonText.text = $"Level {PlayerPrefsHelper.GetCurrentLevel()}";
        }
    }
}
