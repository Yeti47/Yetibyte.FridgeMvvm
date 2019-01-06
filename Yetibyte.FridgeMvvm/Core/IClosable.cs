using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Core
{
    public interface IClosable {

        bool? DialogResult { get; set; }

        event EventHandler Closed;
        event CancelEventHandler Closing;

        void Close();

    }
}
