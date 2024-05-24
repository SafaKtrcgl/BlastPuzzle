using Enums;
using Singleton;
using System;
using System.Linq;
using UnityEngine;

namespace Helper
{
    public class DialogHelper : HelperBase
    {
        [SerializeField] private GenericPopupDialogView popupDialogPrefab;
        [SerializeField] private DialogResource[] dialogResources;

        public T ShowDialog<T>(DialogTypeEnum dialogType) where T : DialogView
        {
            return Instantiate(GetDialogPrefab(dialogType), transform) as T;
        }

        public void ShowGenericPopupDialog(string dialogText, string buttonText, Action buttonAction, Action exitButtonAction)
        {
            var popupDialogView = Instantiate(popupDialogPrefab, transform);
            popupDialogView.Init(dialogText, buttonText, buttonAction, exitButtonAction);
        }

        private DialogView GetDialogPrefab(DialogTypeEnum dialogType)
        {
            return dialogResources.FirstOrDefault(x => x.DialogType == dialogType).DialogPrefab;
        }
    }
}
