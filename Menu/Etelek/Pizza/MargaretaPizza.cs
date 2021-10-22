using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class MargaretaPizza : Pizza
    {
        int Ar
        {
            get; set;
        }
        public MargaretaPizza()
        {
            Ar = 1150;
            Nev = "Margareta pizza";
            ElerhetoMennyiseg = 100;
        }
      
        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg >= 5)
            {
                Ar = 950;
            }
            else
            {
                Ar = 1150;
            }

            return Ar * mennyiseg;
        }
    }
}
