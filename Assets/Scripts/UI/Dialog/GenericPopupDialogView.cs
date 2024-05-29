using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Dialog
{
    public class GenericPopupDialogView : DialogView
    {
        [SerializeField] private TextMeshProUGUI dialogDescriptionText;
        [SerializeField] private TextMeshProUGUI buttonDescriptionText;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button closeButton;

        private UnityAction _confirmButtonListener;
        private UnityAction _exitButtonListener;

        public void Init(string dialogText, string buttonText, Action buttonAction, Action exitButtonAction)
        {
            dialogDescriptionText.text = dialogText;
            buttonDescriptionText.text = buttonText;

            _confirmButtonListener = () => buttonAction?.Invoke();
            _exitButtonListener = () => exitButtonAction?.Invoke();

            confirmButton.onClick.AddListener(_confirmButtonListener);
            closeButton.onClick.AddListener(_exitButtonListener);
            
            transform.DOPunchScale(Vector3.one, .15f);
        }

        private void OnDestroy()
        {
            confirmButton.onClick.RemoveListener(_confirmButtonListener);
            closeButton.onClick.RemoveListener(_exitButtonListener);
        }
    }
}
