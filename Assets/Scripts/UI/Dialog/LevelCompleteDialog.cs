
using DG.Tweening;
using Helper;
using Singleton;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteDialog : DialogView
{
    [SerializeField] private Transform[] transformsToAnimate;
    [SerializeField] private Button continueButton;

    public void Init()
    {
        for (int i = 0; i < transformsToAnimate.Length; i++)
        {
            var transformToAnimate = transformsToAnimate[i];
            transformToAnimate.localScale = Vector3.zero;
            transformToAnimate.DOScale(Vector3.one, .25f).SetDelay(i * 1f).SetEase(Ease.OutBack);
        }
    }

    public void OnBackgroundClick()
    {
        HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper).LoadMainScene();
    }
}
