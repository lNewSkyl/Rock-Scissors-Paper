using System;
using System.Collections.Generic;
using System.Text;

namespace RockScissorsPaper
{
    public class Turn
    {
        public int Button { get; set; }
        public string Name { get; set; }
        public Turn Next { get; set; }
        public Turn Prev { get; set; }
    }
}
