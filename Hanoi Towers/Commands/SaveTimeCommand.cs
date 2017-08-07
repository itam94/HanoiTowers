using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanoi_Towers.VievModels;

using System.Xml.Linq;
using System.Windows;

namespace Hanoi_Towers.Commands
{
    class SaveTimeCommand : ICommand
    {
        private MainViewModel _mainViewModel;
        private XDocument leadboard;

        public SaveTimeCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            leadboard = XDocument.Load("../../Resources/Results.xml");
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            int i = 0;

            if (_mainViewModel.results == null)
                return false;
           
            foreach (XElement result in _mainViewModel.results)
            {
                if (result.Value == "" || 1 > TimeSpan.Compare(_mainViewModel.ResultTime, TimeSpan.Parse(result.Value)))
                {
                    _mainViewModel.IsSaveButtonVisible = true;
                    _mainViewModel.ChangableElement = i;
                    return true;
                }
               i++;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            if(_mainViewModel.UserName == "")
            {
                MessageBox.Show("Write your credentials", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            XElement buffor = new XElement("result");
            bool changed = false;
            for(int i = 0; i < _mainViewModel.results.Count(); i++)
            {
                if ((_mainViewModel.results.ElementAt(i).Value == "" || 1 > TimeSpan.Compare(_mainViewModel.ResultTime , TimeSpan.Parse(_mainViewModel.results.ElementAt(i).Value)))&& !changed)
                {
                    changed = true;
                    buffor = new XElement(_mainViewModel.results.ElementAt(i));
                    _mainViewModel.results.ElementAt(i).Value = _mainViewModel.ResultTime.ToString();
                    _mainViewModel.results.ElementAt(i).Attribute("Name").Value = _mainViewModel.UserName;
                   
                }else if (changed)
                {
                    XElement smallbuf = new XElement(_mainViewModel.results.ElementAt(i));
                    _mainViewModel.results.ElementAt(i).Value = buffor.Value;
                    _mainViewModel.results.ElementAt(i).Attribute("Name").Value = buffor.Attribute("Name").Value;
                    buffor = smallbuf;
                }
            }
            _mainViewModel.leadboard.Save("../../Resources/Results.xml");
            _mainViewModel.results = null;
            List<string> updated = _mainViewModel.ResultList.result;
            updated.Insert(_mainViewModel.ChangableElement, _mainViewModel.UserName + "\n"+ _mainViewModel.ResultTime.ToString());
            updated.RemoveAt(_mainViewModel.ChangableElement+1);
            _mainViewModel.ResultList.result = updated;
            _mainViewModel.IsSaveButtonVisible = false;
            
        }
    }
}
