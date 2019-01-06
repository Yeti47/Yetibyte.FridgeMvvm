using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yetibyte.FridgeMvvm.Commands {

    public class RelayCommand<T> : ICommand {

        private const string ERROR_PARAM_TYPE_MISMATCH = "The command's parameter is not of the correct type. Please make sure it matches the type given as the RelayCommand's type parameter.";

        private readonly Action<T> _executeAction;

        private bool _isExecutable;

        public bool IsExecutable {

            get => _isExecutable;
            set {

                if(_isExecutable != value) {

                    _isExecutable = value;
                    OnCanExecuteChanged();

                }

            }

        }

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> executeAction) => _executeAction = executeAction;

        public bool CanExecute(object parameter) => _isExecutable;

        public void Execute(object parameter) => _executeAction?.Invoke(parameter is T typedParam ? typedParam : throw new ArgumentException(ERROR_PARAM_TYPE_MISMATCH));

        protected virtual void OnCanExecuteChanged(EventArgs eventArgs = null) {

            EventHandler handler = CanExecuteChanged;
            handler?.Invoke(this, eventArgs ?? EventArgs.Empty);

        }

    }

    public class RelayCommand : RelayCommand<object> {

        public RelayCommand(Action<object> executeAction) : base(executeAction) {

        }

    }

}
