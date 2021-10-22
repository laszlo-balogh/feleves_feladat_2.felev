using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class SonGoKuPizza : Pizza
    {
        int Ar
        {
            get; set;
        }
        public SonGoKuPizza()
        {
            Ar = 1450;
            Nev = "Songoku pizza";
            ElerhetoMennyiseg = 100;
        }       

        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg >= 5)
            {
                Ar = 1250;
            }
            else
            {
                Ar = 1450;
            }

            return Ar * mennyiseg;
        }
    }
}
