using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanoi_Towers.VievModels;
using System.Xml.Linq;

namespace Hanoi_Towers.Commands
{
    class LoadResultsCommand : ICommand
    {
        private MainViewModel _mainViewModel;

        public LoadResultsCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            int LastColumnBlocks = Convert.ToInt32(parameter);
            if (LastColumnBlocks == _mainViewModel.SBlockQuantity.Number)
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            _mainViewModel.ResultTime = (DateTime.Now - _mainViewModel.TimeStart);
            foreach (XElement el in _mainViewModel.leadboard.Root.Elements())
            {
                if (el.Attribute("blockQuantity").Value.Equals(_mainViewModel.SBlockQuantity.ToString()))
                {
                    _mainViewModel.results = el.Elements();
                    _mainViewModel.ResultList.LoadResults(_mainViewModel.results, _mainViewModel.SaveTime.CanExecute(null));
                    _mainViewModel.IsResultVisible = true;
                    _mainViewModel.IsStartButtonVisible = true;
                    break;
                }
            }

        }
    }
}
