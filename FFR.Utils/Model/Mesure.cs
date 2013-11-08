using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFR.Parser
{
    class Mesure
    {
        public int id { get; private set; }
        public int size { get; private set; }

        public Mesure(int id, int size)
        {
            this.id = id;
            this.size = size;
        }
    }
}
