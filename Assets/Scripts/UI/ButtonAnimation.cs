using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private const float ScaleFactor = .925f;
        private const float ScaleDuration = .15f;
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one * ScaleFactor, ScaleDuration).SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one, ScaleDuration).SetEase(Ease.OutBack);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerExit(eventData);
        }
    }
}
