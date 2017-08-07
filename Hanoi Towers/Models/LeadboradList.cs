using Hanoi_Towers.VievModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hanoi_Towers.Models
{
    public class LeadboradList : INotifyPropertyChanged
    {
        private List<String> _result;
        public List<String> result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("result");
            }
        }
        MainViewModel _mainViewMode;

        public LeadboradList(MainViewModel mainViewModel)
        {
            _mainViewMode = mainViewModel;
            result = new List<string>();
        }


        public void LoadResults(IEnumerable<XElement> list, bool isChangable)
        {
            if (result.Count > 0)
            {
                result = new List<string>();
            }
            foreach (XElement element in list)
            {
                result.Add(element.Attribute("Name").Value + "\n" + element.Value);
            }
            if (isChangable)
            {
                result.Insert(_mainViewMode.ChangableElement, "");
                result.RemoveAt(result.Count - 1);
            }
            OnPropertyChanged("result");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
