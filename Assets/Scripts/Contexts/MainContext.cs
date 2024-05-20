using Helper;
using Singleton;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Context
{
    public class MainContext : ContextBase
    {
        [SerializeField] private Image arenaBackgroundImage;
        [SerializeField] private LevelButton levelButton;

        public void Start()
        {
            var arenaResource = HelperResources.Instance.GetHelper<ArenaResourceHelper>(HelpersEnum.ArenaResourceHelper).TryGetArenaResource(0);
            arenaBackgroundImage.sprite = arenaResource.ArenaBackgroundSprite;

            levelButton.Init();
        }

        public void OnLevelButtonClicked()
        {
            HelperResources.Instance.GetHelper<ContextHelper>(HelpersEnum.ContextHelper).LoadGameplayScene();
        }
    }
}
