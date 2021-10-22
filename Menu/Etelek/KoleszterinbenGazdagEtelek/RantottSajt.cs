using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class RantottSajt : KoleszterinbenGazdagEtelek
    {
        int Ar
        {
            get; set;
        }
        public RantottSajt()
        {
            Ar = 600;
            Nev = "Rántott sajt";
            ElerhetoMennyiseg = 100;
        }


        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg > 4)
            {
                Ar = 500;
            }
            else
            {
                Ar = 600;
            }
         
            return Ar *mennyiseg;
        }
    }
}
