using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class MalnaKremLeves :TejesEtelek
    {
        int Ar { get; set; }

        public MalnaKremLeves()
        {
            Ar = 720;
            Nev = "Málnakrémleves";
            ElerhetoMennyiseg = 100;
        }

        public override int ElkeszetesiAr(int mennyiseg)
        {

            return Ar * mennyiseg;
        }
    }
}
