using Singleton;
using UnityEngine;

namespace Helper
{
    public class PlayerPrefsHelper : HelperBase
    {
        private readonly string _currentLevelKey = "CurrentCompletedLevel";

        public int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt(_currentLevelKey, 1);
        }

        public void SetCurrentLevel(int level)
        {
            PlayerPrefs.SetInt(_currentLevelKey, level);
        }

    }
}
