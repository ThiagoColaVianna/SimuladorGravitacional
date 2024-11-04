using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorGravitacional
{
    internal class Universo : Corpo
    {
        private List<Corpo> corpos;

        public Universo()
        {
            corpos = new List<Corpo>();
        }
    }
}
