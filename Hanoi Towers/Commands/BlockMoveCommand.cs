using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanoi_Towers.VievModels;
using Hanoi_Towers.Models;
using System.Linq;
using System.ComponentModel;
using System.Windows.Media;




namespace Hanoi_Towers.Commands
{
    internal class BlockMoveCommand : ICommand
    {
        private MainViewModel _mainViewModel;
        private LoadResultsCommand LoadResults;
        private Block LastBlock;

        public BlockMoveCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            LoadResults = new LoadResultsCommand(_mainViewModel);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return !_mainViewModel.IsStartButtonVisible;
        }

        public void Execute(object parameter)
        {
            int column = Convert.ToInt32(parameter);

            findLastBlockOnColumn(column);
            if (!_mainViewModel._IsSelected)
            {
                if (LastBlock == null)
                    return;
                selectBlock();
            }
            else if (_mainViewModel.SelectedBlock != null && _mainViewModel.SelectedBlock.Column != column)
            {
                if (LastBlock == null)
                {
                    _mainViewModel.SelectedBlock.Row = 7;
                    _mainViewModel.SelectedBlock.Column = column;
                }
                else if (_mainViewModel.SelectedBlock.Width > LastBlock.Width)
                {
                    _mainViewModel.SelectedBlock.Row = LastBlock.Row - 1;
                    _mainViewModel.SelectedBlock.Column = column;
                }

                _mainViewModel._IsSelected = false;
                _mainViewModel.SelectedBlock.Color = _mainViewModel.SelectedColour;

                if (LoadResults.CanExecute(lastColumnBlocksCount()))
                {
                    LoadResults.Execute(null);
                    _mainViewModel.changeList();
                }
            }
        }

        private int lastColumnBlocksCount()
        {
            return (from Block in _mainViewModel.Blocks
                    where (Block.Column == 4)
                    select Block).ToList().Count();
        }

        private void selectBlock()
        {
            _mainViewModel.SelectedBlock = LastBlock;
            _mainViewModel._IsSelected = true;
            Color inverting = (Color)ColorConverter.ConvertFromString(_mainViewModel.SelectedColour);
            Color inverted = new Color();
            inverted.ScR = 1.0F - inverting.ScR;
            inverted.ScG = 1.0F - inverting.ScG;
            inverted.ScB = 1.0F - inverting.ScB;
            inverted.ScA = inverting.ScA;
            _mainViewModel.SelectedBlock.Color = inverted.ToString();
        }

        private void findLastBlockOnColumn(int column)
        {
            LastBlock = (from Block in _mainViewModel.Blocks
                         where (Block.Column == column)
                         where (Block.Width >= 0)
                         orderby (Block.Width)
                         select Block).Max();
        }
    }
}
