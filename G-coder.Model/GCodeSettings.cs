using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_coder.Model
{
    public class GCodeSettings
    {
        public int WorkingHeightZ { get; set; } = 0;

        public int SafeHeightZ { get; set; } = 25;

        public int ForwardSpeed { get; set; } = 1000;

    }
}
