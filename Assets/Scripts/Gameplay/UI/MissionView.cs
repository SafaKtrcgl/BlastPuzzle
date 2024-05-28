using DG.Tweening;
using Enums;
using Helper;
using Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionView : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image checkmarkImage;
    [SerializeField] private TextMeshProUGUI countText;

    public bool IsComplete => _count == 0;

    private int _count;
    private Tweener _progressTweener;

    public void Init(ItemTypeEnum itemType)
    {
        itemImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(itemType).ItemSprite(0);
        itemImage.SetNativeSize();
        ((RectTransform)itemImage.transform).sizeDelta /= 2f;
        UpdateCount(1);
    }

    public void UpdateCount(int delta)
    {
        _count += delta;
        if (delta == -1)
        {
            if (_count == 0)
            {
                countText.gameObject.SetActive(false);
                checkmarkImage.gameObject.SetActive(true);
                checkmarkImage.transform.DOScale(Vector3.one, .25f).SetEase(Ease.OutBack);
            }
            else
            {
                _progressTweener?.Kill(true);
                _progressTweener = itemImage.transform.DOShakeScale(.15f);
            }
        }

        countText.text = _count.ToString();
    }
}
