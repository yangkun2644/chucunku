using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace UI.Game2048
{
    class BoardGridLine : Shape 
    {
        GameBoard board;
        //private double width = 0;
        //private double height = 0;



        public double VisiableWidth
        {
            get { return (double)GetValue(VisiableWidthProperty); }
            set { SetValue(VisiableWidthProperty, value); }
        }

        public static readonly DependencyProperty VisiableWidthProperty =
            DependencyProperty.Register("VisiableWidth", typeof(double), typeof(BoardGridLine), new PropertyMetadata(0.0, OnValueChanged));

        public double VisiableHeight
        {
            get { return (double)GetValue(VisiableHeightProperty); }
            set { SetValue(VisiableHeightProperty, value); }
        }

        public static readonly DependencyProperty VisiableHeightProperty =
            DependencyProperty.Register("VisiableHeight", typeof(double), typeof(BoardGridLine), new PropertyMetadata(0.0, OnValueChanged));

         private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardGridLine line = (BoardGridLine)d;
            line.InvalidateVisual();
        }

        public BoardGridLine(GameBoard board)
        {
            this.board = board;
            //board.Children.Add(this);
            board.Children.Insert(0, this);

            Binding binding = new Binding();
            binding.Source = board;
            binding.Path = new PropertyPath("ActualWidth");
            this.SetBinding(VisiableWidthProperty, binding);
            binding = new Binding();
            binding.Source = board;
            binding.Path = new PropertyPath("ActualHeight");
            this.SetBinding(VisiableHeightProperty, binding);
            init();
        }

        private void init()
        {
            SolidColorBrush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#BDAEA5"));
            this.Stroke = brush;
            this.StrokeThickness = 5;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                GeometryGroup gg = new GeometryGroup();
                StreamGeometry sg = new StreamGeometry();
                using (StreamGeometryContext context = sg.Open())
                {
                    for (int i = 1; i <= board.RowCount; i++)
                    {
                        context.BeginFigure(new Point(0, VisiableHeight / board.RowCount * i), true, false);
                        context.LineTo(new Point(VisiableWidth, VisiableHeight / board.RowCount * i), true, true);
                    }

                    for (int i = 1; i <= board.ColumnCount; i++)
                    {
                        context.BeginFigure(new Point(VisiableWidth / board.ColumnCount * i, 0), true, false);
                        context.LineTo(new Point(VisiableWidth / board.ColumnCount * i, VisiableHeight), true, true);
                    }
                }
                sg.Freeze();
                gg.Children.Add(sg);
                return gg;
            }
        }
    }
}
