using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;

namespace Hanoi_Towers.Models
{

    public class Block : INotifyPropertyChanged, IComparable
    {
        public Block(int definedWidth, int definedColumn, int definedRow, GeometryTypes GeomTypeDefined, String color)
        {
            GeomType = GeomTypeDefined;
            Width = definedWidth;
            Color = color;
            Row = definedRow;
            Column = definedColumn;
            setPathGeometry();
        }

        public enum GeometryTypes : int { Rectangle = 1, RoundedRectangle, Ellipse, Diamond };

        public void setPathGeometry()
        {
            if(this.Width < 0)
            {
                PathData = "";
                return;
            }
           switch (GeomType)
            {
                case GeometryTypes.Rectangle:
                    {
                        drawRectangle();
                        break;
                    }
                case GeometryTypes.RoundedRectangle:
                    {
                        drawRoundedRectange();
                        break;
                    }

                case GeometryTypes.Diamond:
                    {
                        drawDiamond();
                        break;
                    }

                case GeometryTypes.Ellipse:
                    {
                        drawEllipse();       
                        break;
                    }
            }
            
        }

        private void drawRectangle()
        {
            var geom = new RectangleGeometry();
            geom.Rect = new Rect(0, 0, 300 - 30 * Width, 60);
            var pathGeometry = PathGeometry.CreateFromGeometry(geom);
            PathData = pathGeometry.ToString().Replace(';', ',');
        }

        private void drawRoundedRectange()
        {
            var geom = new RectangleGeometry();
            geom.Rect = new Rect(0, 0, 300 - 30 * Width, 60);
            geom.RadiusX = 30;
            geom.RadiusY = 30;
            var pathGeometry = PathGeometry.CreateFromGeometry(geom);
            PathData = pathGeometry.ToString().Replace(',', '.').Replace(';', ',');
        }

        private void drawDiamond()
        {
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(160 - 15 * Width, 30);

            LineSegment LineSegment1 = new LineSegment();
            LineSegment LineSegment2 = new LineSegment();
            LineSegment LineSegment3 = new LineSegment();
            LineSegment LineSegment4 = new LineSegment();

            LineSegment1.Point = new Point(320 - 30 * Width, 0);
            LineSegment2.Point = new Point(160 - 15 * Width, -30);
            LineSegment3.Point = new Point(0, 0);
            LineSegment4.Point = new Point(160 - 15 * Width, 30);

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(LineSegment1);
            myPathSegmentCollection.Add(LineSegment2);
            myPathSegmentCollection.Add(LineSegment3);
            myPathSegmentCollection.Add(LineSegment4);
            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;
            PathData = myPathGeometry.ToString().Replace(',', '.').Replace(';', ',');
        }

        private void drawEllipse()
        {
            var geom = new EllipseGeometry();
            geom.Center = new Point((300 - 30 * Width) / 2, -30);
            geom.RadiusX = (300 - 30 * Width) / 2;
            geom.RadiusY = 30;
            var pathGeometry = PathGeometry.CreateFromGeometry(geom);
            PathData = pathGeometry.ToString().Replace(',', '.').Replace(';', ',');
        }

        private Int32 _Width;
        public Int32 Width
        {
            set
            {
                _Width = value;
                OnPropertyChanged("Width");
            }
            get
            {
                return _Width;
            }
        }

        private String _PathData;
        public String PathData
        {
            set
            {
                _PathData = value;
                OnPropertyChanged("PathData");
            }
            get { return _PathData; }
        }

        private int _Column;
        public int Column
        {
            set
            {
                _Column = value;
                OnPropertyChanged("Column");
            }
            get
            {
                return _Column;
            }
        }

        private int _Row;
        public int Row
        {
            set
            {
                _Row = value;
                OnPropertyChanged("Row");
            }
            get
            {
                return _Row;
            }
        }

        private String _Color;

        public event PropertyChangedEventHandler PropertyChanged;

        public String Color
        {
            set
            {
                _Color = value;
                OnPropertyChanged("Color");
            }
            get
            {
                return _Color.ToString();

            }
        }


        public GeometryTypes GeomType { get;  set; }
        private GeometryTypes _GeomType;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public int CompareTo(Object other)
        {
            Block block = (Block)other;
            return this.Width.CompareTo(block.Width);
        }
    }
}
