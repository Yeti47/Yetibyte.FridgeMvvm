using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yetibyte.FridgeMvvm.Services;
using Yetibyte.FridgeMvvm.Utilities;

namespace Yetibyte.FridgeMvvm.WpfExtensions.Dialog {

    public class WpfMessageBoxDialogService : ServiceBase, IDialogService {

        public WpfMessageBoxDialogService(string serviceId = null) : base(serviceId) {

        }

        public DialogResult ShowDialog(string message, string caption, DialogType dialogType = DialogType.Message, DialogButtons dialogButtons = DialogButtons.OK) {

            MessageBoxImage messageBoxImage = GetMessageBoxImageFromDialogType(dialogType);
            MessageBoxButton messageBoxButton = EnumUtil.Convert(dialogButtons, MessageBoxButton.OK);

            return EnumUtil.Convert(MessageBox.Show(message ?? string.Empty, caption ?? string.Empty, messageBoxButton, messageBoxImage), DialogResult.None);

        }

        public void ShowError(string message, string caption = null) => ShowDialog(message, caption, DialogType.Error, DialogButtons.OK);

        public void ShowMessage(string message, string caption = null) => ShowDialog(message, caption);

        private static MessageBoxImage GetMessageBoxImageFromDialogType(DialogType dialogType) {

            switch (dialogType) {
                case DialogType.Message:
                    return MessageBoxImage.None;
                case DialogType.Info:
                    return MessageBoxImage.Information;
                case DialogType.Error:
                    return MessageBoxImage.Error;
                case DialogType.Warning:
                    return MessageBoxImage.Warning;
                case DialogType.Question:
                    return MessageBoxImage.Question;
                case DialogType.Critical:
                    return MessageBoxImage.Stop;
                default:
                    return MessageBoxImage.None;
            }

        } 


    }

}
