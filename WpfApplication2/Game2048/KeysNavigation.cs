using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.Game2048
{
    public class KeysNavigation
    {
        private GameBoard board;

        public KeysNavigation(GameBoard board)
        {
            this.board = board;
        }


        public void canvas(Key k)
        {
            switch (k)
            {
                case Key.Left:
                    board.toLeft();
                    break;
                case Key.Right:
                    board.toRight();
                    break;
                case Key.Up:
                    board.toUp();
                    break;
                case Key.Down:
                    board.toDown();
                    break;
                default:
                    break;
            }
        }


        public void canvas_KeyDown(object sender,KeyEventArgs e)
        {
           
                switch (e.Key)
                {
                    case Key.Left:
                        board.toLeft();
                        break;
                    case Key.Right:
                        board.toRight();
                        break;
                    case Key.Up:
                        board.toUp();
                        break;
                    case Key.Down:
                        board.toDown();
                        break;
                    default:
                        break;
                }
            
        }
    }
}
