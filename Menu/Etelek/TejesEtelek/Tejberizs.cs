using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class Tejberizs : TejesEtelek
    {

        int Ar { get; }

        public Tejberizs()
        {
            Ar = 500;
            Nev = "Tejberizs";
            ElerhetoMennyiseg = 100;
        }
     
        public override int ElkeszetesiAr(int mennyiseg)
        {

            return Ar * mennyiseg;
        }
    }
}
