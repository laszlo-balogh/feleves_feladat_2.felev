using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Menu
{
    abstract class Menu<T>
    {
        public T[] Menupont { get; protected set; } // a választható menüpontokat tartalmazó generikus T tipusú tömb
        public int KivalasztottIndex { get; protected set; } 
        public string MenuCim { get; protected set; }
        public Menu(string menuCim, T[] menupont) 
        {
            this.Menupont = menupont;
            this.MenuCim = menuCim;

            KivalasztottIndex = 0;
        }

        protected abstract void Megjelenites(); 
        public abstract int Inditas();

        public delegate void KivalasztottElemKezelo(int index, T[] tomb); // a kiválaszott index és az a tömb ahonnan leakarjuk kérni a kiválasztott indexet
        public abstract event KivalasztottElemKezelo IndexHozzaadas;     
    }

    class SimaMenu<T> : Menu<T>
    {
        public SimaMenu(string prompt, T[] menupont) : base(prompt, menupont) { }
    
        public override event KivalasztottElemKezelo IndexHozzaadas;       
        protected override void Megjelenites()
        {
            WriteLine(MenuCim);
            for (int i = 0; i < Menupont.Length; i++)
            {
                T aktualisMenupont = Menupont[i];

                if (i == KivalasztottIndex)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {

                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;

                }
                WriteLine($"[{aktualisMenupont.ToString()}]");
            }
            ResetColor();
        }
      
        public override int Inditas()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                Megjelenites();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    KivalasztottIndex--;
                    if (KivalasztottIndex == -1)
                    {
                        KivalasztottIndex = Menupont.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {

                    KivalasztottIndex++;
                    if (KivalasztottIndex == Menupont.Length)
                    {
                        KivalasztottIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            return KivalasztottIndex;
        }      
    }

    class KivalasztosMenu<T> : Menu<T>
    {
        public override event KivalasztottElemKezelo IndexHozzaadas;

        public List<int> KivalasztottElemek { get; private set; } // a kiválaszott indexeket ehez adjuk hozzá, igazából csak az ellenőrzés miatt kell, hogy megnézze h hozzáadtuk e már ezt az elemet
        public KivalasztosMenu(string prompt, T[] menupont) : base(prompt, menupont) { }

        public override int Inditas()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                Megjelenites();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Enter)
                {
                    if (KivalasztottIndex != Menupont.Length - 1 && KivalasztottIndex != Menupont.Length - 2)
                    {
                        if (KivalasztottElemek == null)
                        {
                            KivalasztottElemek = new List<int>();
                        }
                        if (EllenorzesFelvehetoE(KivalasztottElemek, KivalasztottIndex))
                        {
                            KivalasztottElemek.Add(KivalasztottIndex);  // a kiválaszott indexeket ehhez adjuk hozzá, igazából csak az ellenőrzés miatt kell, hogy megnézze h hozzáadtuk e már ezt az elemet

                            IndexHozzaadas?.Invoke(KivalasztottIndex, Menupont); //int triggerelődik az event, és erre az étterem menürendszer osztályában iratkozatok fel metodusokat                          
                        }
                    }
                }
                else if (keyPressed == ConsoleKey.UpArrow)
                {
                    KivalasztottIndex--;

                    if (KivalasztottIndex == -1)
                    {
                        KivalasztottIndex = Menupont.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    KivalasztottIndex++;

                    if (KivalasztottIndex == Menupont.Length)
                    {
                        KivalasztottIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter || (KivalasztottIndex != Menupont.Length - 1 && KivalasztottIndex != Menupont.Length - 2));


            return KivalasztottIndex;
        }

        protected bool EllenorzesFelvehetoE(List<int> lista, int szam) // megnézi h fel lett e már véve az adoot indexű elem, ha nics benne akkor igazat ad vissza, tehát fel lehet venni a listába
        {
            int i = 0;
            while (i < lista.Count && lista[i] != szam)
            {
                i++;
            }

            if (i < lista.Count)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        protected override void Megjelenites()
        {
            int szamlalo = 0;
            WriteLine(MenuCim);
            for (int i = 0; i < Menupont.Length; i++)
            {
                T aktualisMenupont = Menupont[i];

                int szam;

                if (KivalasztottElemek != null)
                {
                    szam = KivalasztottElemek.Count;
                    KivalasztottElemek = (from l in KivalasztottElemek orderby l select l).ToList();
                }
                else
                {
                    szam = 0;
                }
                if (i == KivalasztottIndex)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                    WriteLine($"[{aktualisMenupont.ToString()}]");                                                                     
                    if (szamlalo + 1 < szam)
                    {
                        int j = 0;
                        while (j < KivalasztottElemek.Count - 1 && KivalasztottIndex != KivalasztottElemek[j])
                        {
                            j++;
                        }
                        if (j < KivalasztottElemek.Count - 1)
                        {
                            szamlalo++;
                        }
                    }
                }
                else if (KivalasztottElemek is null)
                {
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                    WriteLine($"[{aktualisMenupont.ToString()}]");
                }
                else if (szam > 0)
                {
                    if (i == KivalasztottElemek[szamlalo])
                    {
                        ForegroundColor = ConsoleColor.White;
                        BackgroundColor = ConsoleColor.Red;
                        WriteLine($"[{aktualisMenupont.ToString()}]");
                        if (szamlalo + 1 < KivalasztottElemek.Count)
                        {
                            szamlalo++;
                        }
                    }
                    else
                    {
                        ForegroundColor = ConsoleColor.White;
                        BackgroundColor = ConsoleColor.Black;
                        WriteLine($"[{aktualisMenupont.ToString()}]");
                    }
                }
                ResetColor();
            }
        }
    }
}
