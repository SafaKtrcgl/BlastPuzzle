using DG.Tweening;
using Helper;
using UnityEngine;

namespace Context
{
    public class GameplayContext : ContextBase
    {
        [SerializeField] private RectTransform topPanelRectTransform;
        [SerializeField] private RectTransform gridCellHolder;

        private const float TopPanelDestinationPosY = 50f;
        private const float PanelAnimationDuration = .5f;

        private void Start()
        {
            PlayInitializationAnimations();
        }

        private void PlayInitializationAnimations()
        {
            topPanelRectTransform.DOAnchorPosY(TopPanelDestinationPosY, PanelAnimationDuration).SetEase(Ease.OutBack);
            gridCellHolder.DOAnchorPosX(0, PanelAnimationDuration).SetEase(Ease.OutBack);
        }
    }
}
