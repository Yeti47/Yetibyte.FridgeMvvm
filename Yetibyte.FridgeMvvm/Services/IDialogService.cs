using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Services {

    public interface IDialogService : IService {

        DialogResult ShowDialog(string message, string caption, DialogType dialogType = DialogType.Message, DialogButtons dialogButtons = DialogButtons.OK);

        void ShowMessage(string message, string caption = null);

        void ShowError(string message, string caption = null);

    }

}
