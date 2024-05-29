using DG.Tweening;
using Enums;
using Helpers;
using UnityEngine;

namespace UI.Dialog
{
    public class LevelCompleteDialog : DialogView
    {
        [SerializeField] private Transform levelCompletedTextTransform;
        [SerializeField] private Transform starImageTransform;
        [SerializeField] private Transform tapToContinueTransform;
        [SerializeField] private Transform particleSystemTransform;

        public void Init()
        {
            levelCompletedTextTransform.localScale = Vector3.zero;
            starImageTransform.localScale = Vector3.one * 2f;
            starImageTransform.gameObject.SetActive(false);
            tapToContinueTransform.localScale = Vector3.zero;

            Sequence InitSequence = DOTween.Sequence();
            InitSequence.Append(levelCompletedTextTransform.DOScale(Vector3.one * 1.25f, .75f).SetEase(Ease.OutBack));
            InitSequence.Append(levelCompletedTextTransform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack).SetDelay(.15f));
            InitSequence.AppendInterval(.75f);
            InitSequence.AppendCallback(() => starImageTransform.gameObject.SetActive(true));
            InitSequence.Append(starImageTransform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack).OnComplete(() => particleSystemTransform.gameObject.SetActive(true)));
            InitSequence.Join(starImageTransform.DOLocalRotate(Vector3.forward * 360, .5f, RotateMode.LocalAxisAdd));
            InitSequence.AppendInterval(1f);
            InitSequence.Append(tapToContinueTransform.DOScale(Vector3.one, .5f));
        }

        public void OnBackgroundClick()
        {
            HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper).LoadMainScene();
        }
    }
}
