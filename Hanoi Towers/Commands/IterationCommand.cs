using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanoi_Towers.VievModels;
using Hanoi_Towers.Models;

namespace Hanoi_Towers.Commands
{
    class IterationCommand : ICommand
    {
        private MainViewModel mainViewModel;
        private List<Block> left;
        private Block Biggest;

        const int Source = 0;
        const int Auxiliary = 2;
        const int Destination = 4;

        public IterationCommand(MainViewModel mainViewModel)
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
            return !mainViewModel.IsStartButtonVisible;
        }

        public void Execute(object parameter)
        {
            List<Block> blocks = (List<Block>)parameter;
            blocks = (from block in blocks where block.Width >= 0 select block).ToList();
            move(blocks, Destination);
            mainViewModel.TimeStart = mainViewModel.TimeStart.AddHours(-1);
        }

        private void move(List<Block> blocks, int toWhere)
        {
            Biggest = blocks.First();
            int onSameColumn = (from block in blocks where block.Column == Biggest.Column select block).Count() - 1;
            int onDestinationColumn = (from block in blocks where block.Column == toWhere select block).Count();

            if (blocks.Count == 1)
            {
                Biggest.Column = toWhere;
                setRows();
                return;
            }

            left = blocks.GetRange(1, blocks.Count - 1);

            if (Biggest.Column == toWhere)
            {
                move(left, toWhere);
            }
            else
            {
                if (onSameColumn > 0 || onDestinationColumn > 0)
                {
                    moveToOtherColumn(toWhere);
                }
                else
                {
                    Biggest.Column = toWhere;
                    setRows();
                }
            }

        }

        private void moveToOtherColumn(int firstDestination)
        {
            switch (firstDestination)
            {
                case Destination:
                    if (Biggest.Column == Auxiliary)
                    {
                        move(left, Source);
                    }
                    else
                    {
                        move(left, Auxiliary);
                    }
                    break;
                case Auxiliary:
                    if (Biggest.Column == Destination)
                    {
                        move(left, Source);
                    }
                    else
                    {
                        move(left, Destination);
                    }
                    break;
                case Source:
                    if (Biggest.Column == Auxiliary)
                    {
                        move(left, Destination);
                    }
                    else
                    {
                        move(left, Auxiliary);
                    }

                    break;
            }
        }

        private void setRows()
        {
            foreach (Block block in mainViewModel.Blocks)
            {
                block.Row = 7- (from b in mainViewModel.Blocks where b.Column == block.Column where b.Width < block.Width where b.Width >= 0 select block).Count();
            }
        }
    }
}
