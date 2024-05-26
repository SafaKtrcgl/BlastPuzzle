using Helper;
using Singleton;
using System.IO;
using UnityEngine;
using Utility;

namespace Context
{
    public class InitializationContext : ContextBase
    {
        private void Start()
        {
            Config.LevelCount = Directory.GetFiles(Config.LevelDataPath, Config.LevelDataPattern).Length;
            
            var contextHelper = HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper);

            
            if (string.IsNullOrEmpty(PlayerPrefsUtility.GetOnGoingLevelData()))
            {
                contextHelper.LoadMainScene();
            }
            else
            {
                contextHelper.LoadGameplayScene();
            }
        }
    }
}
