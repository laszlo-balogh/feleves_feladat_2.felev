using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class DobosTorta : Edesseg
    {
        int Ar { get; set; }

        public DobosTorta()
        {
            Ar = 1000;
            Nev = "Dobostorta";
            ElerhetoMennyiseg = 100;
        }


        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg >= 10)          
            {
                Ar = 900;
            }
            else
            {
                Ar = 1000;
            }

            return Ar * mennyiseg;
        }
    }
}
