using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;
using System;
using System.Windows.Data;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Media.Animation;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace UI.Game2048
{
    public class ColorBlock : Shape , IMovable
    {
       

        private string displayText = Settings.textArr[0];

        private int xIndex = 0;
        private int yIndex = 0;
       // private double xCoo = 0;
       // private double yCoo = 0;
        private double centerXCoo = 0;
        private double centerYCoo = 0;
        private double radius = 5;
        private double width = 100;
        private double height = 100;

        private GameBoard board = null;
        SolidColorBrush foreGroundBrush = Brushes.White;

        public int XIndex
        {
            get
            {
                return xIndex;
            }
        }

        public int YIndex
        {
            get
            {
                return yIndex;
            }
        }

        public double CanvasWidth
        {
            get { return (double)GetValue(CanvasWidthProperty); }
            set { SetValue(CanvasWidthProperty, value); }
        }

        public static readonly DependencyProperty CanvasWidthProperty =
            DependencyProperty.Register("CanvasWidth", typeof(double), typeof(ColorBlock), new PropertyMetadata(0.0, OnValueChanged));

        public double CanvasHeight
        {
            get { return (double)GetValue(CanvasHeightProperty); }
            set { SetValue(CanvasHeightProperty, value); }
        }

        public static readonly DependencyProperty CanvasHeightProperty =
            DependencyProperty.Register("CanvasHeight", typeof(double), typeof(ColorBlock), new PropertyMetadata(0.0, OnValueChanged));

        public int TextIndex
        {
            get { return (int)GetValue(TextIndexProperty); }
            set { SetValue(TextIndexProperty, value); }
        }

        public static readonly DependencyProperty TextIndexProperty =
            DependencyProperty.Register("TextIndex", typeof(int), typeof(ColorBlock), new PropertyMetadata(2, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorBlock line = (ColorBlock)d;
            line.calcCoo();
            line.refresh();
        }

        public double XCoo
        {
            get { return (double)GetValue(XCooProperty); }
            set { SetValue(XCooProperty, value); }
        }

        public static readonly DependencyProperty XCooProperty =
            DependencyProperty.Register("XCoo", typeof(double), typeof(ColorBlock), new PropertyMetadata(0.0, OnCooValueChanged));

        public double YCoo
        {
            get { return (double)GetValue(YCooProperty); }
            set { SetValue(YCooProperty, value); }
        }

        public static readonly DependencyProperty YCooProperty =
            DependencyProperty.Register("YCoo", typeof(double), typeof(ColorBlock), new PropertyMetadata(0.0, OnCooValueChanged));

        private static void OnCooValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorBlock line = (ColorBlock)d;
            if (e.Property == XCooProperty)
                line.refreshLocation((double)e.NewValue, line.YCoo);
            else if (e.Property == YCooProperty)
                line.refreshLocation(line.XCoo, (double)e.NewValue);
            line.refresh();
        }

        public ColorBlock(GameBoard board)
        {
           
            this.board = board;
            board.Children.Add(this);
            initStyle();

            Binding binding = new Binding();
            binding.Source = board;
            binding.Path = new PropertyPath("ActualWidth");
            this.SetBinding(CanvasWidthProperty, binding);
            Binding binding1 = new Binding();
            binding1.Source = board;
            binding1.Path = new PropertyPath("ActualHeight");
            this.SetBinding(CanvasHeightProperty, binding1);

            initLocation();
        }

        private void initStyle()
        {
            this.Stroke = Brushes.Green;
            this.Fill = Settings.textArrBrush[0];
            this.StrokeThickness = 0;
        }

        private void initLocation()
        {
            Random rd = new Random(GetRandomSeed());
            xIndex = rd.Next(board.ColumnCount);
            yIndex = rd.Next(board.RowCount);
            while (board.getState(xIndex, yIndex) > 0)
            {
                xIndex = rd.Next(board.ColumnCount);
                yIndex = rd.Next(board.RowCount);
            }

            board.setState(xIndex, yIndex, this);
            calcCoo();
            refresh();
            Canvas.SetLeft(this, this.XCoo);
            Canvas.SetTop(this, this.YCoo);
        }

        void calcCoo()
        {
            XCoo = CanvasWidth / board.ColumnCount * xIndex;
            YCoo = CanvasHeight / board.RowCount * yIndex;
        }

        void refresh()
        {
            this.InvalidateVisual();
        }

        void refreshLocation(double x, double y)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            this.InvalidateVisual();
        }

        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                GeometryGroup gg = new GeometryGroup();
                width = CanvasWidth / board.ColumnCount - 6;
                height = CanvasHeight / board.RowCount - 6;
                RectangleGeometry rect = new RectangleGeometry(new Rect(3, 3, width, height), radius, radius);
                gg.Children.Add(rect);
                return gg;  
            }
        }

        FormattedText formattedText;
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            formattedText = new FormattedText(displayText,
                     CultureInfo.GetCultureInfo("en-us"),
                     FlowDirection.LeftToRight,
                     new Typeface("幼圆"),
                     Math.Min(width,height) / 4, foreGroundBrush);
            centerXCoo = width / 2 - formattedText.Width / 2 + 3;
            centerYCoo = height / 2 - formattedText.Height / 2 + 3;
            drawingContext.DrawText(formattedText, new Point(centerXCoo, centerYCoo));
        }

        public bool moveLeft()
        {
            if (leftIsBoundary())
            {
                return false;
            }
            if (leftHasBlock() && !isSame(Direction.Left))
            {
                return false;
            }
            if (leftHasBlock() && isSame(Direction.Left))
            { 
                //合并
                bool ret = board.union(xIndex, yIndex, Direction.Left);
                if (!ret)
                    return true;
                animLeft();
                moveLeft();
                refresh();
            }
            if (!leftHasBlock() && !leftIsBoundary())
            {
                animLeft();
                moveLeft();
                refresh();
            }
            return true;
        }

        private void animLeft()
        {
            //左移
            board.setState(xIndex, yIndex, null);
            xIndex--;
            board.setState(xIndex, yIndex, this);

            double oldXCoo = XCoo;
            double oldYCoo = YCoo;
            double xCoo1 = CanvasWidth / board.ColumnCount * xIndex;
            double yCoo1 = CanvasHeight / board.RowCount * yIndex;
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(da_Completed);
            da.FillBehavior = FillBehavior.Stop;
            da.DecelerationRatio = 0.5;
            da.From = oldXCoo;
            da.To = xCoo1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(XCooProperty, da);
        }

        public bool moveRight()
        {
            if (rightIsBoundary())
            {
                return false;
            }
            if (rightHasBlock() && !isSame(Direction.Right))
            {
                return false;
            }
            if (rightHasBlock() && isSame(Direction.Right))
            {
                //合并
                bool ret = board.union(xIndex, yIndex, Direction.Right);
                if (!ret)
                    return true;

                animRight();
                moveRight();
                refresh();
            }
            if (!rightHasBlock() && !rightIsBoundary())
            {
                animRight();
                moveRight();
                refresh();
            }
            return true;
        }
        private void animRight()
        {
            //右移
            board.setState(xIndex, yIndex, null);
            xIndex++;
            board.setState(xIndex, yIndex, this);
            //calcCoo();

            double oldXCoo = XCoo;
            double oldYCoo = YCoo;
            double xCoo1 = CanvasWidth / board.ColumnCount * xIndex;
            double yCoo1 = CanvasHeight / board.RowCount * yIndex;
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(da_Completed);
            da.FillBehavior = FillBehavior.Stop;
            da.DecelerationRatio = 0.5;
            da.From = oldXCoo;
            da.To = xCoo1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(XCooProperty, da);
        }

        public bool moveUp()
        {
            if (upIsBoundary())
            {
                return false;
            }
            if (upHasBlock() && !isSame(Direction.Up))
            {
                return false;
            }
            if (upHasBlock() && isSame(Direction.Up))
            {
                bool ret = board.union(xIndex, yIndex, Direction.Up);
                if (!ret)
                    return true;
                animUp();
                moveUp();
                refresh();
            }
            if (!upHasBlock() && !upIsBoundary())
            {
                //上移
                board.setState(xIndex, yIndex, null);
                yIndex--;
                board.setState(xIndex, yIndex, this);
                //calcCoo();
                animUp();
                moveUp();
                refresh();
            }
            return true;
        }

        private void animUp()
        {
            double oldXCoo = XCoo;
            double oldYCoo = YCoo;
            double xCoo1 = CanvasWidth / board.ColumnCount * xIndex;
            double yCoo1 = CanvasHeight / board.RowCount * yIndex;
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(da_Completed);
            da.FillBehavior = FillBehavior.Stop;
            da.DecelerationRatio = 0.5;
            da.From = oldYCoo;
            da.To = yCoo1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(YCooProperty, da);
        }

        public bool moveDown()
        {
            if (downIsBoundary())
            {
                return false;
            }
            if (downHasBlock() && !isSame(Direction.Down))
            {
                return false;
            }
             if (downHasBlock() && isSame(Direction.Down))
            {
                //合并
                bool ret = board.union(xIndex, yIndex, Direction.Down);
                if (!ret)
                    return true;
                animDown();
                moveDown();
            }
            if (!downHasBlock() && !downIsBoundary())
            {
                animDown();
                moveDown();
                refresh();
            }
            return true;
        }

        private void animDown()
        {
            //下移
            board.setState(xIndex, yIndex, null);
            yIndex++;
            board.setState(xIndex, yIndex, this);
            //calcCoo();

            double oldXCoo = XCoo;
            double oldYCoo = YCoo;
            double xCoo1 = CanvasWidth / board.ColumnCount * xIndex;
            double yCoo1 = CanvasHeight / board.RowCount * yIndex;
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(da_Completed);
            da.FillBehavior = FillBehavior.Stop;
            da.DecelerationRatio = 0.5;
            da.From = oldYCoo;
            da.To = yCoo1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(YCooProperty, da);
        }

        /// <summary>
        /// 属性动画会绑定属性，造成无法修改的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void da_Completed(object sender, EventArgs e)
        {
            //解决方法，等待完成后重新赋值
            calcCoo();
            refreshLocation(XCoo,YCoo);
            this.refresh();
        }

        private bool isSame(Direction direct)
        {
            bool ret = false;
            switch (direct)
            {
                case Direction.Left:
                    ret = board.getState(xIndex - 1, yIndex) == TextIndex;
                    break;
                case Direction.Right:
                    ret = board.getState(xIndex + 1, yIndex) == TextIndex;
                    break;
                case Direction.Up:
                    ret = board.getState(xIndex, yIndex - 1) == TextIndex;
                    break;
                case Direction.Down:
                    ret = board.getState(xIndex, yIndex + 1) == TextIndex;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        private bool leftHasBlock()
        {
            //Debug.WriteLine("leftHasBlock");
            return board.isLocationFilled(xIndex-1,yIndex);
        }

        private bool leftIsBoundary()
        {
            //Debug.WriteLine("leftHasBlock");
            return xIndex==0;
        }

        private bool rightHasBlock()
        {
            //Debug.WriteLine("rightHasBlock");
            return board.isLocationFilled(xIndex+1, yIndex);
        }

        private bool rightIsBoundary()
        {
            //Debug.WriteLine("rightIsBoundary");
            return xIndex==board.RowCount;
        }

        private bool upHasBlock()
        {
            //Debug.WriteLine("upHasBlock");
            return board.isLocationFilled(xIndex, yIndex - 1);
        }

        private bool upIsBoundary()
        {
            //Debug.WriteLine("upHasBlock");
            return yIndex == 0;
        }

        private bool downHasBlock()
        {
            //Debug.WriteLine("downHasBlock");
            return board.isLocationFilled(xIndex, yIndex + 1);
        }

        private bool downIsBoundary()
        {
            //Debug.WriteLine("downHasBlock");
            return yIndex == board.RowCount;
        }

        private byte convertColor(byte n)
        {
            if (n >= 0 && n <= 127)
                return (byte)(n + 127);
            else
                return (byte)(n - 127);
        }

        public bool changeText()
        {
            this.TextIndex = this.TextIndex*2;
            displayText = Settings.textArr[((int)Math.Log(TextIndex, 2) - 1) % Settings.textArr.Length];
            int brushIndex = ((int)Math.Log(TextIndex, 2) - 1) % Settings.textArr.Length;
            if(brushIndex>Settings.textArrBrush.Length-1)
            {
                Random random = new Random(GetRandomSeed());
                Color color = Color.FromRgb((byte)random.Next(255),(byte)random.Next(255),(byte)random.Next(255));
                foreGroundBrush = new SolidColorBrush(Color.FromRgb(convertColor(color.R), convertColor(color.G), convertColor(color.B)));
                SolidColorBrush brush = new SolidColorBrush(color);
                List<Brush> list = new List<Brush>();
                list.AddRange(Settings.textArrBrush);
                list.Add(brush);
                Settings.textArrBrush = list.ToArray();
                this.Fill = brush;
            }
            else
                this.Fill = Settings.textArrBrush[brushIndex];
            this.InvalidateVisual();
            if (TextIndex >= Math.Pow(2, Settings.textArr.Length))
            {
                //结束
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
