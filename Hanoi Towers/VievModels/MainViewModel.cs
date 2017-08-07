using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Hanoi_Towers.Commands;
using Hanoi_Towers.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Xml.Linq;

namespace Hanoi_Towers.VievModels
{

    public enum GeometryTypes : int { Rectangle = 1, RoundedRectangle, Ellipse, Diamond };

    public class MainViewModel : INotifyPropertyChanged
    {
        public bool _IsSelected;
        public bool _IsEnded;
        public const int MAX_BLOCK_QUANTITY = 7;
        public Block SelectedBlock;
        public DateTime TimeStart;
        public TimeSpan ResultTime;
        public XDocument leadboard;

        public MainViewModel()
        {
            BlockMove = new BlockMoveCommand(this);
            SaveTime = new SaveTimeCommand(this);
            Iteration = new IterationCommand(this);
            StartGame = new StartGameCommand(this);
            NewGame = new NewGameCommand(this);
            _Blocks = new List<Block>();

            IsSaveButtonVisible = false;
            _IsSelected = false;
            _IsEnded = false;
            IsStartButtonVisible = true;


            SGeometryType = GeometryTypes.Diamond;
            SelectedColour = Colors.Black.ToString();
            UserName = "";
            leadboard = XDocument.Load("../../Resources/Results.xml");
            ResultList = new LeadboradList(this);

            BlockQuantites = new ObservableCollection<BlockQuantityModel>() { new BlockQuantityModel(7), new BlockQuantityModel(6), new BlockQuantityModel(5), new BlockQuantityModel(4), new BlockQuantityModel(3) };
            SBlockQuantity = new BlockQuantityModel(7);
            fillList();
        }

        private bool _IsSaveButtonVisible;
        public bool IsSaveButtonVisible
        {
            set
            {
                _IsSaveButtonVisible = value;
                OnPropertyChanged("IsSaveButtonVisible");
            }
            get
            {
                return _IsSaveButtonVisible;
            }
        }

              public bool IsResultVisible
        {
            set
            {
                _IsEnded = value;
                OnPropertyChanged("IsResultVisible");
            }
            get
            {
                return _IsEnded;
            }
        }

        private bool _IsStartButtonVisible;
        public bool IsStartButtonVisible
        {
            set
            {
                _IsStartButtonVisible = value;
                OnPropertyChanged("IsStartButtonVisible");
            }
            get
            {
                return _IsStartButtonVisible;
            }
        }

        private int _ChangableElement;
        public int ChangableElement
        {
            set
            {
                _ChangableElement = value;
                OnPropertyChanged("ChangableElement");
            }
            get
            {
                return _ChangableElement;
            }
        }

        private GeometryTypes _SGeometryType;
        public GeometryTypes SGeometryType
        {
            set
            {
                _SGeometryType = value;
                changeList();
            }
            get
            {
                return _SGeometryType;
            }
        }

        private IEnumerable<XElement> _results;
        public IEnumerable<XElement> results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged("results");
            }
        }

        private String _UserName;
        public String UserName
        {
            set { _UserName = value; }
            get { return _UserName; }
        }

        private ObservableCollection<BlockQuantityModel> _BlockQuantietes;
        public ObservableCollection<BlockQuantityModel> BlockQuantites
        {
            get { return _BlockQuantietes; }
            set
            {
                _BlockQuantietes = value;
                OnPropertyChanged("BlockQuantites");
            }
        }

        private BlockQuantityModel _SBlockQuantity;
        public BlockQuantityModel SBlockQuantity
        {
            get { return _SBlockQuantity; }
            set
            {
                _SBlockQuantity = value;


                changeList();
                OnPropertyChanged("SBlockQuantity");
            }
        }

        public void changeList()
        {
            IsStartButtonVisible = true;
            if (Blocks.Count == 0)
                return;
            for (int i = 0; i < MAX_BLOCK_QUANTITY; i++)
            {
                if (i < _SBlockQuantity.Number)
                {
                    Blocks[i].Width = i;
                }
                else
                {
                    Blocks[i].Width = -1;
                }
                Blocks[i].GeomType = (Block.GeometryTypes)_SGeometryType;
                Blocks[i].setPathGeometry();
                Blocks[i].Column = 0;
                Blocks[i].Row = MAX_BLOCK_QUANTITY - i;
                Blocks[i].Color = SelectedColour;

            }
            _IsSelected = false;
        }

        private void fillList()
        {
            for (int i = 0; i < MAX_BLOCK_QUANTITY; i++)
            {
                if (i < _SBlockQuantity.Number)
                {

                    Blocks.Add(new Block(i, 0, MAX_BLOCK_QUANTITY - i, (Block.GeometryTypes)SGeometryType, SelectedColour));
                }
                else
                {
                    Blocks.Add(new Block(-10, 0, MAX_BLOCK_QUANTITY - i, (Block.GeometryTypes)SGeometryType, SelectedColour));
                }
            }

        }

        private List<Block> _Blocks;
        public List<Block> Blocks
        {
            get
            {
                return _Blocks;
            }
        }

        public ICommand BlockMove
        {
            get;
            set;
        }

        public ICommand NewGame
        {
            get;
            set;
        }

        public ICommand SaveTime
        {
            get;
            set;
        }
        public ICommand StartGame
        {
            get;
            set;
        }

        public ICommand Iteration
        {
            get;
            set;
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

        private LeadboradList _ResultList;
        public LeadboradList ResultList
        {
            get
            {
                return _ResultList;
            }
            set { _ResultList = value;
                OnPropertyChanged("ResultList");
            }
        }

        private String _SelectedColour;
        public String SelectedColour
        {
            set
            {
                _SelectedColour = value.ToString();
                changeList();
            }
            get
            {
                return _SelectedColour;
            }
        }

        private List<String> _listaznakow;
        public List<String> listaznakow
        {
            get {
                if (_listaznakow == null)
                    _listaznakow.Add("mama");
                return _listaznakow;
            }
        }


    }
}
