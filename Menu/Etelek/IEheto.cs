using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public delegate void TulNagyRendelesKezelo(string kulcs);
    interface IEheto 
    {
        event TulNagyRendelesKezelo NemElerheto;
        string Nev { get; }
        int ElerhetoMennyiseg { get; set; }

        void ElerhetoMennyisegCsokkent(int szam);
        void ElerhetoMennyisegCsokkent();

        bool Fogyaszthatja(string betegseg);

        int ElkeszetesiAr(int mennyiseg);
        string ToString();
    }
}
