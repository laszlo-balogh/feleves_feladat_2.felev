using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class PiskotaTekercs : Edesseg
    {
        int Ar { get; set; }

        public PiskotaTekercs()
        {
            Ar = 300;
            Nev = "Piskótatekercs";
            ElerhetoMennyiseg = 100;
        }

        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg > 40)
            {
              
                return Ar * 40;
            }
            else
            {
               
                return Ar * mennyiseg;
            }

        }
    }
}
