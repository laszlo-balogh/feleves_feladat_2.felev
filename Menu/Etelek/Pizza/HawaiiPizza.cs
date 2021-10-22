using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class HawaiiPizza : Pizza
    {
        int Ar { get; set; }

        public HawaiiPizza()
        {
            Ar = 1500;
            Nev = "Hawaii pizza";
            ElerhetoMennyiseg = 100;
        }
       
        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg >= 5)
            {
                Ar = 1300;
            }
            else
            {
                Ar = 1500;
            }

            return Ar * mennyiseg;
        }
    }
}
