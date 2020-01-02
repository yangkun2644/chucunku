using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Diagnostics;

namespace UI.Game2048
{
    public class GameBoard : Canvas, IControlable
    {
        private int rowCount = 4;
        
        public int RowCount
        {
            get
            {
                return rowCount;
            }
            set
            {
                rowCount = value;
            }
        }

        private int columnCount = 4;
        public int ColumnCount 
        {
            get
            {
                return columnCount;
            }
            set
            {
                columnCount = value;
            }
        }

        Game game = null;

        public GameBoard()
        {
            this.Focusable = true;
            this.Focus();
            this.reset();
        }

        public void reset()
        {
            Settings.load();
            RowCount = Settings.rowCount;
            ColumnCount = Settings.columnCount;
        }

        public void init(Game game)
        {
            this.game = game;
            game.init();
        }

        public void toLeft()
        {
            game.toLeft();
            Debug.WriteLine("Left");
        }

        public void toRight()
        {
            game.toRight();
            Debug.WriteLine("Right");
        }

        public void toUp()
        {
            game.toUp();
            Debug.WriteLine("Up");
        }

        public void toDown()
        {
            game.toDown();
            Debug.WriteLine("Down");
        }

        //合并,是否继续
        public bool union(int xIndex, int yIndex, Direction dirct)
        {
            switch (dirct)
            {
                case Direction.Left:
                    game.remove(xIndex - 1, yIndex);
                    break;
                case Direction.Right:
                    game.remove(xIndex + 1, yIndex);
                    break;
                case Direction.Up:
                    game.remove(xIndex, yIndex - 1);
                    break;
                case Direction.Down:
                    game.remove(xIndex, yIndex + 1);
                    break;
                default:
                    break;
            }
            bool ret = game.FillState[yIndex, xIndex].changeText();
            if (ret)
            {
                game.gameOver(true);
                return false;
            }
            game.incScore(game.FillState[yIndex, xIndex].TextIndex);
            return true;
        }

        public int getState(int xIndex, int yIndex)
        {
            if (xIndex < 0 || xIndex > columnCount - 1)
                return 0;
            if (yIndex < 0 || yIndex > rowCount - 1)
                return 0;
            if (game.FillState[yIndex,xIndex] == null)
                return 0;
            return game.FillState[yIndex, xIndex].TextIndex;
        }

        public bool hasNoPlace()
        {
            return this.Children.Count == this.RowCount * this.ColumnCount+1;
        }

        public bool isLocationFilled(int xIndex,int yIndex)
        {
            if (xIndex < 0 || xIndex > columnCount-1)
                return true;
            if (yIndex < 0 || yIndex > rowCount-1)
                return true;
            if (game.FillState[yIndex, xIndex] == null)
                return false;
            return game.FillState[yIndex, xIndex].TextIndex>0;
        }

        public void setState(int xIndex,int yIndex,ColorBlock block)
        {
            game.FillState[yIndex, xIndex] = block;
        }
    }
}
