using System;
using System.Windows.Input;

namespace Hanoi_Towers.VievModels
{
    internal class StartGameCommand : ICommand
    {
        private MainViewModel mainViewModel;

        public StartGameCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {

            return !mainViewModel.IsSaveButtonVisible;
        }

        public void Execute(object parameter)
        {
            mainViewModel.IsStartButtonVisible = false;
            mainViewModel.IsResultVisible = false;
            mainViewModel.TimeStart = DateTime.Now;
          
        }
    }
}