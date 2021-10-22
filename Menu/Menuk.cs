using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Menu
{
    class EtteremMenuRendszer
    {
        BinarisKresoFa<IEheto, string> etelekFaja; 
        BinarisKresoFa<IEheto, string> kivalaszottetelekfaja;
        int hanyadikCsoport;
        AznapiCsoport Csoport { get; set; } 

        public EtteremMenuRendszer()
        {
            etelekFaja = new BinarisKresoFa<IEheto, string>();
            hanyadikCsoport = 0;
        }      

        public void Start()
        {
            FaFeltoltes();
            FoMenu();
        }

        void FoMenu()
        {
            string[] Menupont = new string[] { "Csoport adatok megadása", "Csoport adatok beolvasása fileból", "Kilépés" };

            string Cim = "Menu Rendszer";

            Menu<string> fomenu = new SimaMenu<string>(Cim, Menupont);
            int selectedIndex = fomenu.Inditas();
            switch (selectedIndex)
            {
                case 0:
                    //a fát nem hozzuk létre újra, ezáltal csak azok az elemek lesznek benne
                    // amik maradtak, de új csoport az jöhet tehát itt ki kell nullázni a csoportot és felvenni egy újat
                    Csoport = new AznapiCsoport();
                    CsoportMenu();
                    break;
                case 1:
                    Csoport = new AznapiCsoport();
                    FilebolOlvasas();
                    MenuEsEtelek();
                    break;
                case 2:
                    break;
            }
        }

        private void FilebolOlvasas()
        {
            string sor = "";
            int index = 0;
            string[] sorTomb = null;
            StreamReader sr = new StreamReader("csoportAdatok.txt");
            while (!sr.EndOfStream && index <= hanyadikCsoport)
            {
                sor = sr.ReadLine();
                if (index == hanyadikCsoport)
                {
                    sorTomb = sor.Split(',');
                }
                index++;

            }
            sr.Close();
            if (sorTomb == null)
            {
                throw new ArgumentException("Szöveges file hiba: Nincs több csoport a file-ban!");
            }
            Csoport.Nev = sorTomb[0];
            bool szamE = int.TryParse(sorTomb[1], out int letszam);
            if (szamE)
            {
                if (letszam < 0)
                {
                    throw new NegativSzamException(1, sorTomb[1]);
                }
                else if (letszam == 0) { }
                else
                {
                    Csoport.Letszam = letszam;
                }
            }
            else
            {
                throw new NemSzamException(1, sorTomb[1]);
            }
            for (int i = 2; i < sorTomb.Length; i++)
            {
                Csoport.betegsegek.Add(sorTomb[i]);
            }
            hanyadikCsoport++;
        }

        private void CsoportMenu()
        {
            string[] Menupont = new string[] { "Név", "Létszám", "Betegségek", "Vissza" };
            Menu<string> csoportmenu = new SimaMenu<string>("Csoport Menu", Menupont);
            int selectedIndex = csoportmenu.Inditas();
            switch (selectedIndex)
            {
                case 0:
                    Clear();
                    Csoport.Nev = ReadLine();
                    CsoportMenu();
                    break;
                case 1:
                    Clear();
                    bool szamE = int.TryParse(ReadLine(), out int beolvasottSzam);
                    while (!szamE || beolvasottSzam < 0)
                    {
                        if (!szamE)
                        {
                            Clear();
                            WriteLine("Nem számot adtál meg!");
                            szamE = int.TryParse(ReadLine(), out beolvasottSzam);
                        }
                        else
                        {
                            Clear();
                            WriteLine("Negatív számot adtál meg!");
                            szamE = int.TryParse(ReadLine(), out beolvasottSzam);
                        }
                    }
                    if (beolvasottSzam == 0) { }
                    else
                    {
                        Csoport.Letszam = beolvasottSzam;
                    }
                    CsoportMenu();
                    break;
                case 2:
                    Csoport.betegsegek = new List<string>();
                    BetegsegekMegadas();
                    break;
                case 3:
                    FoMenu();
                    break;
            }
        }

        private void BetegsegBeszur(int index, string[] menupont) //ez van feliratkoztatva a Menu eventjére, hozzáadja a csoport betegségeinek a listájához, a kiválaszott betegséget
        {
            Csoport.betegsegek.Add(menupont[index]);
        }
        private void BetegsegekMegadas()
        {

            string[] Menupont = new string[] { "Koleszterin", "Cukorbetegség", "Laktózérzékenység", "Gluténérzékenység", "Tovább", "Vissza" };
            Menu<string> Betegsegek = new KivalasztosMenu<string>("Betegségek kiválasztása Menü", Menupont);

            Betegsegek.IndexHozzaadas += BetegsegBeszur;

            int selectedIdnex = Betegsegek.Inditas();

            switch (selectedIdnex)
            {
                case 0:
                    BetegsegekMegadas();
                    break;
                case 1:
                    BetegsegekMegadas();
                    break;
                case 2:
                    BetegsegekMegadas();
                    break;
                case 3:
                    BetegsegekMegadas();
                    break;
                case 4:
                    MenuEsEtelek();
                    break;
                case 5:
                    CsoportMenu();
                    break;
            }
        }
       
        private void MenuEsEtelek()
        {
            string[] Menupont = new string[] { "Optimális Menü", "Ételek rendelése", "Vissza" };
            Menu<string> MenuEsEtelek = new SimaMenu<string>("Menü/Ételek kiválasztása", Menupont);
            int selectedIndex = MenuEsEtelek.Inditas();
            bool van = false;
            IEheto[] optimalis = new IEheto[Csoport.Letszam];
            IEheto[] e = new IEheto[Csoport.Letszam];
            switch (selectedIndex)
            {
                case 0:
                    OptimalisMenuBTS(FureszFogasVisszaAd(Szures(), Csoport.Letszam), 0, e, ref van, ref optimalis);                    
                    VegOsszeg(VegOsszegKiszamitas(optimalis), optimalis);
                    break;
                case 1:
                    EtelekKivalasztasa();
                    break;
                case 2:
                    CsoportMenu();
                    break;
            }
        }

        private IEheto[][] FureszFogasVisszaAd(BinarisKresoFa<IEheto, string> fa, int letszam)
        {
            IEheto[][] etelek = new IEheto[letszam][];
            IEheto[] etelekTomb = fa.ToArray();

            for (int i = 0; i < letszam; i++)
            {
                etelek[i] = etelekTomb;
            }
            return etelek;
        }

        void OptimalisMenuBTS(IEheto[][] r, int szint, IEheto[] e, ref bool van, ref IEheto[] opt)
        {
            int db = 0;
            bool voltE = false;
            int szamlalo = 0;
            for (int j = 0; j < r[0].Length; j++)
            {
                r[0][j].NemElerheto += etelekFaja.Torles;
            }
            IEheto[] optimalis = VisszaLepesesKereses(r, szint, e, ref van, ref opt);
            for (int i = 0; i < optimalis.Length; i++)
            {
                if (i >= szint) // csak azokat az elemeket kell csökkenteni amiket most teszünk bele, és ugye csak egy bizonyos szint felett rakunk bele új elemeket
                {
                    if (optimalis[i].ElerhetoMennyiseg == 0)
                    {
                        int l = i;
                        while (l < optimalis.Length && optimalis[i] == optimalis[l])
                        {
                            l++;
                        }
                        if (l < optimalis.Length)
                        {
                            szamlalo++;
                        }

                        if (!voltE)
                        {
                            voltE = true;
                            db = i;
                        }
                        if (szamlalo > 0)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                optimalis[i].ElerhetoMennyiseg++;
                            }
                        }
                        if (optimalis[i].ElerhetoMennyiseg == 0)
                        {
                            optimalis[i].ElerhetoMennyisegCsokkent();
                        }

                    }
                    if (db == 0)
                    {
                        optimalis[i].ElerhetoMennyisegCsokkent(); // meg kell nézni h befojásolta-e a keresést és ha igen akkor újra kell keresni
                    }
                }
            }

            if (db != 0 && voltE && szamlalo == 0)
            {
                OptimalisMenuBTS(r,db, optimalis, ref van, ref optimalis);
            }
            else if (db != 0 && voltE && szamlalo > 0)
            {
                OptimalisMenuBTS(r, 0, optimalis, ref van, ref optimalis);
            }
        }

        int IndexVisszadasH(IEheto[] tomb, IEheto etel)
        {
            int i = 0;
            while (i < tomb.Length && tomb[i] != etel)
            {
                i++;
            }

            if (i < tomb.Length)
            {
                return i;
            }
            else
            {
                throw new ArgumentException("Nincs ilyen étel a tömbben");
            }
        }

        public double Josag(IEheto[] vizsgalandoTomb, IEheto[] alapTomb)
        {
            int l = 0;
            while (l < vizsgalandoTomb.Length && vizsgalandoTomb[l] != null)
            {
                l++;
            }
            if (l < vizsgalandoTomb.Length)
            {
                return double.PositiveInfinity;
            }

            int[] etelMennyiseg = new int[alapTomb.Length];
            int ar = 0;

            for (int i = 0; i < vizsgalandoTomb.Length; i++)
            {
                if (vizsgalandoTomb.Length == 1)
                {
                    etelMennyiseg[0]++;
                }
                else
                {
                    int index = IndexVisszadasH(alapTomb, vizsgalandoTomb[i]);  // visszaadja h az eredmények tömb i-edik eleme hanyadik indexen van a bemeneti fureszfogas tömb egyik tömbjében              
                    etelMennyiseg[index]++;   //miből mennyi van
                }
            }
            for (int i = 0; i < alapTomb.Length; i++)
            {
                if (vizsgalandoTomb.Length == 1)
                {
                    ar += vizsgalandoTomb[0].ElkeszetesiAr(etelMennyiseg[0]);
                }
                else
                {
                    ar += alapTomb[i].ElkeszetesiAr(etelMennyiseg[IndexVisszadasH(alapTomb, alapTomb[i])]);
                }
            }
            double doubleAr = Convert.ToDouble(ar);
            return doubleAr;
        }

        private IEheto[] VisszaLepesesKereses(IEheto[][] r, int szint, IEheto[] e, ref bool van, ref IEheto[] opt)
        {
            int maxRendeles = 0;
            for (int z = 0; z < r[0].Length; z++)
            {
                maxRendeles += r[0][z].ElerhetoMennyiseg;
            }
            if (r.Length>maxRendeles)
            {
                throw new NincsElegEtelException(maxRendeles, r.Length);
            }
            int i = -1;
            while (i < r[szint].Length - 1)
            {
                i++;
                if (Fk(szint, r[szint][i], e))
                {
                    e[szint] = r[szint][i];
                    if (szint == r.Length - 1)
                    {
                        if (!van || Josag(e, r[0]) < Josag(opt, r[0]))
                        {

                            for (int k = 0; k < opt.Length; k++)
                            {
                                opt[k] = e[k];
                            }
                        }
                        van = true;
                    }
                    else
                    {
                        VisszaLepesesKereses(r, szint + 1, e, ref van, ref opt);
                    }
                }
            }
            return opt;
        }

        bool Fk(int szint, IEheto etel, IEheto[] e)
        {

            bool ok = true;
            if (etel.ElerhetoMennyiseg > 0) { }
            else
            {
                ok = false;
            }

            return ok;
        }
       
        private void KivalasztottEtelekHozzaadasa(int index, string[] tomb) //ez van feliratkoztatva a Menu eventjére, ez adja hozzá a kiválasztott IEheto ételt amit rendeltek
        {
            IEheto[] etelek = Szures().ToArray();

            for (int i = 0; i < etelek.Length; i++)
            {
                if (etelek[i].Nev == tomb[index])
                {
                    kivalaszottetelekfaja.Beszuras(etelek[i], etelek[i].Nev);
                }
            }
        }

        private void EtelekKivalasztasa() 
        {
            kivalaszottetelekfaja = new BinarisKresoFa<IEheto, string>();

            IEheto[] etelek = Szures().ToArray();
            string[] etelekNeveiTomb = new string[etelek.Length + 2];

            for (int i = 0; i < etelek.Length; i++)
            {
                etelekNeveiTomb[i] = etelek[i].Nev;
            }

            etelekNeveiTomb[etelekNeveiTomb.Length - 2] = "Megrendel";
            etelekNeveiTomb[etelekNeveiTomb.Length - 1] = "Vissza";

            Menu<string> etelekMenu = new KivalasztosMenu<string>("Szűrt étlap", etelekNeveiTomb);

            etelekMenu.IndexHozzaadas += KivalasztottEtelekHozzaadasa;
            int selectedIndex = etelekMenu.Inditas();
            IEheto[] kivalasztottEtelekTomb = kivalaszottetelekfaja.ToArray();
            int[] mennyisegek = new int[kivalasztottEtelekTomb.Length];
            int kirandoTombMeret = 0;
            int osszeg = MennyisegMegadasa(ref mennyisegek, ref kirandoTombMeret);
            IEheto[] kiIrandoTomb = new IEheto[kirandoTombMeret];
            int k = 0;

            for (int j = 0; j < mennyisegek.Length; j++)
            {
                int db = 0;
                while (db < mennyisegek[j])
                {
                    db++;
                    kiIrandoTomb[k] = kivalasztottEtelekTomb[j];
                    k++;
                }
            }
            ;
            if (selectedIndex == etelekNeveiTomb.Length - 2)
            {
                VegOsszeg(osszeg, kiIrandoTomb);

            }
            if (selectedIndex == etelekNeveiTomb.Length - 1)
            {
                CsoportMenu();
            }
        }

        private int VegOsszegKiszamitas(IEheto[] tomb)
        {
            int ar = 0;
            int db = 0;
            List<int> mennyisegek = new List<int>();
            for (int i = 0; i < tomb.Length; i++)
            {
                if (i == 0)
                {
                    mennyisegek.Add(1);
                }
                else if (tomb[i] == tomb[i - 1])
                {
                    mennyisegek[mennyisegek.Count - 1]++;
                }
                else
                {
                    mennyisegek.Add(1);
                }
            }
            int j = 0;
            while (db < mennyisegek.Count && j < tomb.Length)
            {
                ar += tomb[j].ElkeszetesiAr(mennyisegek[db]);
                db++;
                if (db < mennyisegek.Count)
                {
                    j += mennyisegek[db - 1];
                }

            }
            return ar;
        }

        private void VegOsszeg(int szam, IEheto[] tomb)
        {
            int vegosszeg = szam;
            string[] Menupont = new string[] { "Ételek megnézése", "Menü elejére", "Kilépés" };
            Menu<string> VegOsszeg = new SimaMenu<string>($"{Csoport.Nev} \nVégösszeg: \t {vegosszeg} FT", Menupont);
            int selectedIndex = VegOsszeg.Inditas();
            switch (selectedIndex)
            {
                case 0:
                    EtelekMegnezese(tomb);
                    break;
                case 1:
                    FoMenu();
                    break;
                case 2:
                    break;
            }
        }

        private void EtelekMegnezese(IEheto[] tomb)
        {
            string[] Menupont = new string[] { "Menü elejére", "Kilépés" };
            string cim = "Megrendelt ételek:\n\n";
            for (int i = 0; i < tomb.Length; i++)
            {
                cim += $"{tomb[i].ToString()}\n";
            }
            Menu<string> EtelekMegnezeseMenu = new SimaMenu<string>(cim, Menupont);
            int selectedIndex = EtelekMegnezeseMenu.Inditas();
            switch (selectedIndex)
            {
                case 0:
                    FoMenu();
                    break;
                case 1:
                    break;
            }
        }

        private BinarisKresoFa<IEheto, string> Szures() // visszaadja azokat az ételeket, amiket fogyaszthat a csoport
        {
            BinarisKresoFa<IEheto, string> szurtEtelek = new BinarisKresoFa<IEheto, string>(); // a betegségek alapján kiválogatott, megmaradt ételek          

            etelekFaja.Bejaras = BinarisKresoFa<IEheto, string>.BejarasModja.PreOrder; // azért nem inorder, hogy ne egy "láncolt listát" kapjunk
            if (Csoport.betegsegek.Count == 0) // ha nincs betegség a listában,ne ellenőrizgessen felelslegesen addja vissza az ételeket
            {
                foreach (IEheto item in etelekFaja)
                {
                    szurtEtelek.Beszuras(item, item.Nev);
                }
            }
            else
            {
                foreach (IEheto item in etelekFaja)
                {
                    bool voltEMarFalse = false; //ha már az egyik étel Fogyaszthatja metódusára false-t adott vissza akkor ezt eltároljuk, mert ugye akkor nem eheti a csoport                    
                    int i = 0;
                    if (item is SemlegesEtelek)// itt se ellenőrizgessen a FogyaszthatjaE metódus alapján mivel semleges étel így mindenki fogyaszthatja
                    {
                        szurtEtelek.Beszuras(item, item.Nev);
                    }
                    else
                    {
                        //ha a valotEMarFalse igaz akkor kilép mert felesleges leellenőrizni a többi betegségre ugyanazt az ételt
                        //egyrészt true-t fog visszaadni a fogyaszthatja metódus, másrészt hozzáadni ugyse fogjuk, mert már nem fogyaszthatják mivel false-t adott vissza a FogyaszthatjaE
                        while (i < Csoport.betegsegek.Count && voltEMarFalse == false)
                        {
                            if (!item.Fogyaszthatja(Csoport.betegsegek[i]))
                            {
                                voltEMarFalse = true;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        if (voltEMarFalse == false) // nem kaptunk vissza false-t a Fogyaszthatja metódusból akkor megfelelő a csoport számára tehát hozzáadjuk
                        {
                            szurtEtelek.Beszuras(item, item.Nev);
                        }
                    }
                }
            }

            return szurtEtelek;
        }

        private int MennyisegMegadasa(ref int[] tomb, ref int mennyiseg) //itt adjuk meg a kiválaszott elemekből h mennyit akarunk rendelni
        {
            int indexer = 0;
            int szam = 0;

            List<int> etelekMennyisegek = new List<int>();
            kivalaszottetelekfaja.Bejaras = BinarisKresoFa<IEheto, string>.BejarasModja.InOrder;
            foreach (IEheto etel in kivalaszottetelekfaja)
            {

                etel.NemElerheto += etelekFaja.Torles; // ha 100 rendelünk tehát elfogy az elérhető mennyiség, törölje az elemet ilyenkor is ne csak az optimális menünél
                Clear();
                WriteLine($"{etel.ToString()} | elérhető mennyiség: {etel.ElerhetoMennyiseg}");
                bool szamE = int.TryParse(ReadLine(), out int beolvasottSzam);
                while (!szamE || beolvasottSzam > etel.ElerhetoMennyiseg || beolvasottSzam < 0)
                {
                    if (!szamE)
                    {
                        Clear();
                        WriteLine($"{etel.ToString()} | elérhető mennyiség: {etel.ElerhetoMennyiseg}");
                        WriteLine("Nem szamot adtal meg!");
                    }
                    else if (beolvasottSzam > etel.ElerhetoMennyiseg)
                    {
                        Clear();
                        WriteLine($"{etel.ToString()} | elérhető mennyiség: {etel.ElerhetoMennyiseg}");
                        WriteLine("Többet adtál meg, mint ami elérhető!");
                    }
                    else
                    {
                        Clear();
                        WriteLine($"{etel.ToString()} | elérhető mennyiség: {etel.ElerhetoMennyiseg}");
                        WriteLine("Negatív számot adtál meg!");
                    }

                    szamE = int.TryParse(ReadLine(), out beolvasottSzam);
                }
                mennyiseg += beolvasottSzam;
                etel.ElerhetoMennyisegCsokkent(beolvasottSzam);
                etelekMennyisegek.Add(beolvasottSzam);
                tomb[indexer] = beolvasottSzam;
                indexer++;
                Clear();
            }
            indexer = 0;
            foreach (IEheto etel in kivalaszottetelekfaja)
            {
                szam += etel.ElkeszetesiAr(etelekMennyisegek[indexer]);
                indexer++;
            }

            return szam;
        }

        public void EtelHozzaAdas(IEheto etel)
        {
            etelekFaja.Beszuras(etel, etel.Nev);
        }

        private void FaFeltoltes()
        {
            IEheto hawaiiPizza = new HawaiiPizza();
            IEheto magyarosPizza = new MagyarosPizza();
            IEheto margaretaPizza = new MargaretaPizza();
            IEheto mexikoiPizza = new MexikoiPizza();
            IEheto songokuPizza = new SonGoKuPizza();

            IEheto dobosTorta = new DobosTorta();
            IEheto kokuszGolyo = new KokuszGolyo();
            IEheto piskotaTekercs = new PiskotaTekercs();

            IEheto rantottSajt = new RantottSajt();
            IEheto tojasRantotta = new TojasRantotta();
            IEheto ciganyPecsenye = new CiganyPecsenye();

            IEheto halfile = new Halfile();
            IEheto poharViz = new PoharViz();
            IEheto spagetti = new Spagetti();

            IEheto brokkoliKremLeves = new BrokkoliKremLeves();
            IEheto malnaKremLeves = new MalnaKremLeves();
            IEheto tejbeRizs = new Tejberizs();

            etelekFaja.Beszuras(hawaiiPizza, hawaiiPizza.Nev);
            etelekFaja.Beszuras(magyarosPizza, magyarosPizza.Nev);
            etelekFaja.Beszuras(margaretaPizza, margaretaPizza.Nev);
            etelekFaja.Beszuras(mexikoiPizza, mexikoiPizza.Nev);
            etelekFaja.Beszuras(songokuPizza, songokuPizza.Nev);
            etelekFaja.Beszuras(dobosTorta, dobosTorta.Nev);
            etelekFaja.Beszuras(kokuszGolyo, kokuszGolyo.Nev);
            etelekFaja.Beszuras(piskotaTekercs, piskotaTekercs.Nev);
            etelekFaja.Beszuras(rantottSajt, rantottSajt.Nev);
            etelekFaja.Beszuras(tojasRantotta, tojasRantotta.Nev);
            etelekFaja.Beszuras(ciganyPecsenye, ciganyPecsenye.Nev);
            etelekFaja.Beszuras(halfile, halfile.Nev);
            etelekFaja.Beszuras(poharViz, poharViz.Nev);
            etelekFaja.Beszuras(spagetti, spagetti.Nev);
            etelekFaja.Beszuras(brokkoliKremLeves, brokkoliKremLeves.Nev);
            etelekFaja.Beszuras(malnaKremLeves, malnaKremLeves.Nev);
            etelekFaja.Beszuras(tejbeRizs, tejbeRizs.Nev);
        }
    }
}