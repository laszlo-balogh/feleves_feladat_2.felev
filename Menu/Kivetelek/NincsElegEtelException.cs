using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class NincsElegEtelException:Exception
    {
        public NincsElegEtelException(int megmaradt,int letszam) : base($"Kiszolgálás hiba: nincs elég étel a csoport kiszolgálásához. Megfelelő megmaradt ételek szama: {megmaradt}, csopoprt létszáma {letszam}!")
        {

        }
    }
}
