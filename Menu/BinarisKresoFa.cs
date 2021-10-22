using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class BinarisKresoFa<T, K> : IEnumerable<T> where K : IComparable
    {
        public delegate bool SzuresHandler(K betegseget);

        public enum BejarasModja { InOrder, PreOrder, PostOrder }

        class FaElem
        {
            public T tartalom;
            public K kulcs;
            public FaElem jobb;
            public FaElem bal;
        }

        private FaElem gyoker;

        private BejarasModja bejaras;
        public BejarasModja Bejaras { set { bejaras = value; } }

        private IEnumerable<T> Tartalom
        {
            get {

                List<T> tmp = new List<T>();
                switch (bejaras)
                {
                    case BejarasModja.InOrder:
                    default:
                        InOrderBejaras(tmp, gyoker);
                        break;
                    case BejarasModja.PreOrder:
                        PreOrderBejaras(tmp, gyoker);
                        break;
                    case BejarasModja.PostOrder:
                        PostOrderBejaras(tmp, gyoker);
                        break;

                }
                return tmp;
            }
        }

        public bool Tartalmaz(K kulcs)
        {
            return Tartalmaz(gyoker, kulcs);
        }

        private bool Tartalmaz(FaElem p,K kulcs)
        {
            if (p != null)
            {
                if (p.kulcs.CompareTo(kulcs) < 0)
                {                  
                    return Tartalmaz(p.jobb, kulcs);
                }
                else if (p.kulcs.CompareTo(kulcs) > 0)
                {                   
                    return Tartalmaz(p.bal, kulcs);
                }
                else
                {                   
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void Beszuras(T tartalom, K kulcs)
        {
            if (tartalom.ToString()!=kulcs.ToString())
            {
                throw new NemEgyezoException<T, K>(tartalom,kulcs);
            }
            Beszuras(ref gyoker, tartalom, kulcs);
        }
        private void Beszuras(ref FaElem p, T tartalom, K kulcs)
        {
            if (p == null)
            {
                p = new FaElem();
                p.tartalom = tartalom;
                p.kulcs = kulcs;
            }
            else
            {
                if (p.kulcs.CompareTo(kulcs) < 0)
                {
                    Beszuras(ref p.jobb, tartalom, kulcs);
                }                
                else if (p.kulcs.CompareTo(kulcs) > 0)
                {
                    Beszuras(ref p.bal, tartalom, kulcs);
                }
                else
                {
                    throw new ArgumentException("Binaris keresőfa hiba:A beszúrni kívánt elem kulcsával egyező kulcs már van a fában");
                }
            }
        }

        public void Torles(K kulcs)
        {
            Torles(ref gyoker, kulcs);
        }

        private void Torles(ref FaElem p, K kulcs)
        {
            FaElem q;
            if (p != null)
            {
                if (p.kulcs.CompareTo(kulcs) > 0)
                {
                    Torles(ref p.bal, kulcs);
                }
                else
                {
                    if (p.kulcs.CompareTo(kulcs) < 0)
                    {
                        Torles(ref p.jobb, kulcs);
                    }
                    else
                    {
                        if (p.bal == null)
                        {
                            q = p;
                            p = p.jobb;
                        }
                        else
                        {
                            if (p.jobb == null)
                            {
                                q = p;
                                p = p.bal;
                            }
                            else
                            {
                                TorlesKetGyerek(p, ref p.bal);
                            }
                        }
                    }
                }
            }
        }

        private void TorlesKetGyerek(FaElem e, ref FaElem r)
        {
            FaElem q;
            if (r.jobb != null)
            {
                TorlesKetGyerek(e, ref r.jobb);
            }
            else
            {
                e.tartalom = r.tartalom;
                e.kulcs = r.kulcs;
                q = r;
                r = r.bal;
            }
        }


        private void InOrderBejaras(List<T> lista, FaElem p)
        {
            if (p != null)
            {
                InOrderBejaras(lista, p.bal);
                lista.Add(p.tartalom);
                InOrderBejaras(lista, p.jobb);
            }
        }

        private void PreOrderBejaras(List<T> lista, FaElem p)
        {
            if (p != null)
            {
                lista.Add(p.tartalom);
                PreOrderBejaras(lista, p.bal);
                PreOrderBejaras(lista, p.jobb);
            }
        }
        private void PostOrderBejaras(List<T> lista, FaElem p)
        {
            if (p != null)
            {
                PostOrderBejaras(lista, p.bal);
                PostOrderBejaras(lista, p.jobb);
                lista.Add(p.tartalom);
            }
        }
        public List<T> ToList() //InOrder sorrenben adja vissza
        {
            List<T> lista = new List<T>();
            this.bejaras = BejarasModja.InOrder;
            foreach (T item in this)
            {
                lista.Add(item);
            }
            return lista;
        }

        public T[] ToArray()//InOrder sorrenben adja vissza
        {
            List<T> lista = new List<T>();
            this.bejaras = BejarasModja.InOrder;
            foreach (T item in this)
            {
                lista.Add(item);
            }
            return lista.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Tartalom.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Tartalom.GetEnumerator();
        }
    }
}
