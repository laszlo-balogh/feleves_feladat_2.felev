using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class BrokkoliKremLeves:TejesEtelek
    {
        int Ar { get; set; }

        public BrokkoliKremLeves()
        {
            Ar = 840;
            Nev = "Brokkolikrémleves ";
            ElerhetoMennyiseg = 100;
        }


        public override int ElkeszetesiAr(int mennyiseg)
        {

            return Ar * mennyiseg;
        }

    }
}
