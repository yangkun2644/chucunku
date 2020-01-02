using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Game2048
{

    public class Game2048ViewsMasterMenuItem
    {
        public Game2048ViewsMasterMenuItem()
        {
            TargetType = typeof(Game2048ViewsMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}