using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class Spagetti:SemlegesEtelek
    {
        int Ar { get; set; }

        public Spagetti()
        {
            Ar = 1980;            
            Nev = "Bolognai spagetti";
            ElerhetoMennyiseg = 100;
        }


        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg == 2)
            {
               
                return 0;
            }
            else
            {

                return Ar * mennyiseg;
            }

           
        }
    }
}
