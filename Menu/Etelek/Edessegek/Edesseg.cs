using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    abstract class Edesseg : IEheto
    {
        public string Nev { get; protected set; }
        public int ElerhetoMennyiseg { get; set; }
        public abstract int ElkeszetesiAr(int mennyiseg);

        public event TulNagyRendelesKezelo NemElerheto;
        public void ElerhetoMennyisegCsokkent(int szam)
        {

            if (ElerhetoMennyiseg - szam >= 0)
            {
                if (ElerhetoMennyiseg - szam == 0)
                {
                    NemElerheto?.Invoke(this.Nev);
                }
                ElerhetoMennyiseg -= szam;
            }
        }
        public void ElerhetoMennyisegCsokkent()
        {
            if (ElerhetoMennyiseg <= 0)
            {
                NemElerheto?.Invoke(this.Nev);
            }

            if (ElerhetoMennyiseg - 1 >= 0)
            {
                ElerhetoMennyiseg--;
            }
        }

        public bool Fogyaszthatja(string betegseg)
        {
            if (betegseg == "Cukorbetegség")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public override string ToString()
        {
            return Nev;
        }

       
    }
}
