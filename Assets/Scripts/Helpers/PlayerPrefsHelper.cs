using Singleton;
using UnityEngine;

namespace Helper
{
    public class PlayerPrefsHelper : HelperBase
    {
        private const string _currentLevelKey = "CurrentCompletedLevel";

        public static int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt(_currentLevelKey, 1);
        }

        public static void SetCurrentLevel(int level)
        {
            PlayerPrefs.SetInt(_currentLevelKey, level);
        }
    }
}
