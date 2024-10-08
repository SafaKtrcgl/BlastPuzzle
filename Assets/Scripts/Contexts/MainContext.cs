using Enums;
using Helpers;
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
            var arenaResource = HelperResources.Instance.GetHelper<ArenaResourceHelper>(HelperEnum.ArenaResourceHelper).TryGetArenaResource(0);
            if (arenaResource != null )
            {
                arenaBackgroundImage.sprite = arenaResource.ArenaBackgroundSprite;
            }

            levelButton.Init();
        }

        public void OnLevelButtonClicked()
        {
            HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper).LoadGameplayScene();
        }
    }
}
