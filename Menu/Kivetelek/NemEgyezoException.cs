using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class NemEgyezoException<T,K> :Exception
    {
        public NemEgyezoException(T tartalom, K kulcs):base($"Binaris keresőfa hiba: Nem a megfelelő kulcsot({kulcs}) adtad meg a tartalomhoz({tartalom})!")
        {

        }
    }
}
