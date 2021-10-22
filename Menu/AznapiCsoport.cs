using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class AznapiCsoport
    {

        public string Nev { get;  set; }
        public int Letszam { get;  set; }       

        public List<string> betegsegek = new List<string>();

        public AznapiCsoport()
        {
            this.Letszam = 1;
            this.Nev = "Csoport";
        }       
    }
}
