using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class PoharViz : SemlegesEtelek
    {
        int Ar { get; }

        public PoharViz()
        {
            Ar = 50;
            Nev = "Pohár víz";
            ElerhetoMennyiseg = 100;
            
        }

        public override int ElkeszetesiAr(int mennyiseg)
        {         
            return Ar * mennyiseg;            
        }
    }
}
