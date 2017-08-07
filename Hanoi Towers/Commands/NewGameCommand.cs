using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanoi_Towers.VievModels;

namespace Hanoi_Towers.Commands
{
    class NewGameCommand : ICommand
    {
        private MainViewModel mainViewModel;

        public NewGameCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mainViewModel.changeList();
        }
    }
}
