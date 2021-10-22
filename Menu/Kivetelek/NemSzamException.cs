using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class NemSzamException : Exception
    {
        public NemSzamException(int pozicio, string szo):base($"Szöveges fájl hiba: \nNem számot adtál meg az {pozicio}. vessző után: {szo} !")
        {

        }
    }
}
