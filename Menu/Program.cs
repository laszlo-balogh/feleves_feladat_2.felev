using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class Program
    {                    
        static void Main(string[] args)
        {
            try
            {
                EtteremMenuRendszer m = new EtteremMenuRendszer();
                m.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }



        }
    }
}