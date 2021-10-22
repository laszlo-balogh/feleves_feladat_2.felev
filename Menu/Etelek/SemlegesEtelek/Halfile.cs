using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class Halfile:SemlegesEtelek
    {
        int Ar { get; set; }

        public Halfile()
        {
            Ar = 1740;            
            Nev = "Alaszkai tőkehalfilé roston";
            ElerhetoMennyiseg = 100;
        }
     
        public override int ElkeszetesiAr(int mennyiseg)
        {

            return Ar * mennyiseg;
        }
    }
}
