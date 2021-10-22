using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class TojasRantotta : KoleszterinbenGazdagEtelek
    {
        int Ar
        {
            get; set;
        }
        public TojasRantotta()
        {
            Ar = 350;
            Nev = "Tojásrántotta 4 tojásból";
            ElerhetoMennyiseg = 100;
        }
       
        public override int ElkeszetesiAr(int mennyiseg)
        {
            if (mennyiseg > 50)
            {
                return Ar * 50;
            }
            else
            {
                return Ar * mennyiseg;
            }
           
        }
    }
}
