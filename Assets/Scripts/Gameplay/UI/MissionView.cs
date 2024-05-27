using Enums;
using Helper;
using Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionView : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI countText;

    public bool IsComplete => count == 0;

    private int count = -1;

    public void Init(ItemTypeEnum itemType)
    {
        itemImage.sprite = HelperResources.Instance.GetHelper<ItemResourceHelper>(HelperEnum.ItemResourceHelper).TryGetItemResource(itemType).ItemSprite(0);
        itemImage.SetNativeSize();
        ((RectTransform)itemImage.transform).sizeDelta /= 2f;
        count = 1;
    }

    public void UpdateCount(int difference)
    {
        count += difference;
        countText.text = count.ToString();
    }
}
