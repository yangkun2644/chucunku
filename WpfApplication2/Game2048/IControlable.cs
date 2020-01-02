using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Game2048
{
    interface IControlable
    {
        void toLeft();
        void toRight();
        void toUp();
        void toDown();
    }
}
