using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class KokuszGolyo : Edesseg
    {

        int Ar { get; set; }

        public KokuszGolyo()
        {
            Ar = 40;
            Nev = "Kókuszgolyó";
            ElerhetoMennyiseg = 100;           
        }
   
        public override int ElkeszetesiAr(int mennyiseg)
        {


            if (mennyiseg > 80)
            {
                               
                return Ar * 80;
            }
            else
            {
                
                return Ar * mennyiseg;
            }

        }
    }
}
