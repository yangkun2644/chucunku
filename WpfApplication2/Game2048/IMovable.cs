using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Game2048
{
    public interface IMovable
    {
        bool moveLeft();
        bool moveRight();
        bool moveUp();
        bool moveDown();
    }
}
