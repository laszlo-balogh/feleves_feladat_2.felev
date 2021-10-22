using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class MexikoiPizza:Pizza
    {
        int Ar
        {
            get; set;
        }
        public MexikoiPizza()
        {
            Ar = 1750;
            Nev = "Mexikói pizza";
            ElerhetoMennyiseg = 100;
        }

        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg >= 5)
            {
                Ar = 1550;
            }
            else
            {
                Ar = 1750;
            }

            return Ar * mennyiseg;
        }
    }
}
