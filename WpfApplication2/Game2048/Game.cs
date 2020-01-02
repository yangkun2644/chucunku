using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace UI.Game2048
{
    public class Game
    {
        public enum State
        { 
            Idel,
            Start,
            Running,
        }

        ColorBlock[,] fillState;
        private int score = 0;
        private int step = 0;


        public ColorBlock[,] FillState
        { 
            get
            {
                return fillState;
            }
        }

        GameBoard board;

        public Game(GameBoard board)
        {
            this.board = board;
            fillState = new ColorBlock[board.RowCount, board.ColumnCount];
            for (int i = 0; i < board.RowCount; i++)
            {
                for (int j = 0; j < board.ColumnCount; j++)
                {
                    fillState[i, j] = null;
                }
            }
        }

        public void init()
        {
            Settings.load();
            ColorBlock block = new ColorBlock(board);
            ColorBlock block1 = new ColorBlock(board);
            //FillState[block.XIndex, block.YIndex] = block;
           // FillState[block1.XIndex, block1.YIndex] = block1;
            //BlockList.Add(block);
            //BlockList.Add(block1);
        }

        public void addNew()
        {
            if (board.hasNoPlace())
            {
                gameOver(false);
                return;
            }
            ColorBlock block = new ColorBlock(board);
            //FillState[block.XIndex, block.YIndex] = block;
            //BlockList.Add(block);
        }

        public void remove(int xIndex,int yIndex)
        {
            if (FillState[yIndex, xIndex] != null)
            {
                board.Children.Remove(FillState[yIndex, xIndex]);
                FillState[yIndex, xIndex] = null;
            }
        }

        public void toLeft()
        {
            bool add = false;
            for (int i = 0; i < board.ColumnCount; i++)
            {
                for (int j = 0; j < board.RowCount; j++)
                {
                    if (FillState[j, i] != null)
                    {
                        add |= FillState[j, i].moveLeft();
                    }
                }
            }
            if (add)
            {
                addNew();
                fireSetpChanged();
            }
        }

        public void toRight()
        {
            bool add = false;
            for (int i = board.ColumnCount-1; i >=0 ; i--)
            {
                for (int j = 0; j < board.RowCount; j++)
                {
                    if (FillState[j, i] != null)
                    {
                        add |= FillState[j, i].moveRight();
                    }
                }
            }
            if (add)
            {
                addNew();
                fireSetpChanged();
            }
        }

        public void toUp()
        {
            bool add = false;
            for (int i = 0; i < board.ColumnCount; i++)
            {
                for (int j = 0; j < board.RowCount; j++)
                {
                    if (FillState[j, i] != null)
                    {
                        add |= FillState[j, i].moveUp();
                    }
                }
            }
            if (add)
            {
                addNew();
                fireSetpChanged();
            }
        }

        public void toDown()
        {
            bool add = false;
            for (int i = 0; i < board.ColumnCount; i++)
            {
                for (int j = board.RowCount-1; j >=0; j--)
                {
                    if (FillState[j, i] != null)
                    {
                        add |= FillState[j, i].moveDown();
                    }
                }
            }
            if (add)
            {
                addNew();
                fireSetpChanged();
            }
        }

        public delegate void onScoreChange(int score);
        public event onScoreChange onScoreChangeHandler;
        public delegate void onStepChange(int step);
        public event onStepChange onStepChangeHandler;
        public delegate void onGameOver(bool success);
        public event onGameOver onGameOverHandler;

        public void fireSetpChanged()
        {
            step++;
            if (onStepChangeHandler != null)
                onStepChangeHandler(step);
        }

        /// <summary>
        /// 增加积分
        /// </summary>
        /// <param name="offset"></param>
        public void incScore(int offset)
        {
            score += offset;
            if (onScoreChangeHandler != null)
            {
                onScoreChangeHandler(score);
            }
        }

        public void gameOver(bool success)
        {
            if (onGameOverHandler != null)
            {
                onGameOverHandler(success);
            }
        }

        public void reset()
        {
            score = 0;
            step = 0;
            if (onStepChangeHandler != null)
                onStepChangeHandler(step);
            if (onScoreChangeHandler != null)
                onScoreChangeHandler(score);
            for (int i = 0; i < board.RowCount; i++)
            {
                for (int j = 0; j < board.ColumnCount; j++)
                {
                    remove(i, j);
                }
            }
        }
    }
}
