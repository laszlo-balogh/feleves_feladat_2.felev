using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class MagyarosPizza:Pizza
    {
        int Ar { get; set; }

        public MagyarosPizza()
        {
            Ar = 1600;
            Nev = "Magyaros pizza";
            ElerhetoMennyiseg = 100;
        }
     
        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg >= 5)
            {
                Ar = 1400;
            }
            else
            {
                Ar = 1600;
            }

            return Ar * mennyiseg;
        }
    }
}
