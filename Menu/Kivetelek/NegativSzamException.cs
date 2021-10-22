using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class NegativSzamException: Exception
    {
        public NegativSzamException(int pozicio,string szo):base($"Szöveges fájl hiba: \nNegatív számot adtál meg az {pozicio}. vessző után: {szo} !")
        {

        }
    }
}
