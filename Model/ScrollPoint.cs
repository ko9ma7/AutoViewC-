using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoViewWebAdsCSharp.Model
{
    public class ScrollPoint : SimplePoint
    {
        public int TargetX { get; set; }

        public int TargetY { get; set; }

        public int Duration { get; set; } = 10;

    }
}
