using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class CiganyPecsenye : KoleszterinbenGazdagEtelek
    {
        int Ar { get; }
        public CiganyPecsenye()
        {
            Ar = 1900;
            Nev = "Cigánypecsenye";
            ElerhetoMennyiseg = 100;
        }
      
        public override int ElkeszetesiAr(int mennyiseg)
        {
           
            return Ar * mennyiseg;
        }
    }
}
