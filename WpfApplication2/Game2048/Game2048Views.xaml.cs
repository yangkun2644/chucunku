using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Game2048
{
    /// <summary>
    /// Game2048Views.xaml 的交互逻辑
    /// </summary>
    public partial class Game2048Views : UserControl
    {
        Game game = null;
        BoardGridLine grid = null;
        KeysNavigation keysNavigation = null;

        public Game2048Views()
        {
            InitializeComponent();
            Focusable = true;     //可接受焦点，必须先设这个属性     

            Focus();  //尝试获得焦点
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            keysNavigation = new KeysNavigation(board);
            this.KeyDown += new KeyEventHandler(keysNavigation.canvas_KeyDown);
            this.MouseDown += new MouseButtonEventHandler(MainWindow_MouseDown);
            this.MouseMove += new MouseEventHandler(MainWindow_MouseMove);
            this.MouseUp += new MouseButtonEventHandler(MainWindow_MouseUp);
        }

        void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPanning = false;
        }

        void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //if (isPanning)
                //    this.DragMove();
            }
            catch (Exception)
            {

            }
        }

        bool isPanning = false;
        void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isPanning = true;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            grid = new BoardGridLine(board);
            initGame();
        }

        private void initGame()
        {
            game = new Game(board);
            board.init(game);
            game.onScoreChangeHandler += new Game.onScoreChange(game_onScoreChangeHandler);
            game.onStepChangeHandler += new Game.onStepChange(game_onStepChangeHandler);
            game.onGameOverHandler += new Game.onGameOver(game_onGameOverHandler);
        }

        void game_onGameOverHandler(bool success)
        {
            MessageBox.Show("Game Over");
            game.reset();
        }

        void game_onStepChangeHandler(int step)
        {
            stepText.Text = step.ToString();
        }

        void game_onScoreChangeHandler(int Score)
        {
            scoreText.Text = Score.ToString();
        }

        private void optionBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
        }

        private void newBtn_Click(object sender, RoutedEventArgs e)
        {
            scoreText.Text = "0";
            stepText.Text = "0";
            board.reset();
            board.Children.Clear();
            grid = new BoardGridLine(board);
            initGame();
        }
        
        private void shang_Click(object sender, RoutedEventArgs e)
        {
            keysNavigation.canvas(Key.Up);
        }

        private void xia_Click(object sender, RoutedEventArgs e)
        {
            keysNavigation.canvas(Key.Down);
        }

        private void zuo_Click(object sender, RoutedEventArgs e)
        {
            keysNavigation.canvas(Key.Left);
        }

        private void yuo_Click(object sender, RoutedEventArgs e)
        {
            keysNavigation.canvas(Key.Right);

        }

        //private void exitBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    //this.Close();
        //}

        //private void min_Click(object sender, RoutedEventArgs e)
        //{
        //    //this.WindowState = System.Windows.WindowState.Minimized;
        //}

        //private void max_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.WindowState == System.Windows.WindowState.Maximized)
        //        this.WindowState = System.Windows.WindowState.Normal;
        //    else
        //        this.WindowState = System.Windows.WindowState.Maximized;
        //}

        //private void close_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
