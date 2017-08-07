using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Hanoi_Towers.Models
{
    public class BlockQuantityModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BlockQuantityModel(int selectedNumber)
        {
            _number = selectedNumber;
        }

        private int _number;
        public int Number
        {
            set { _number = value;
                OnPropertyChanged("Number");
            }
            get { return _number; }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        override
        public String ToString()
        {
            return _number.ToString();
        }
    }
}
