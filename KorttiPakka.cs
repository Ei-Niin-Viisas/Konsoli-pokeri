namespace PokeriPeli
{
    internal class MaanMukaan : IComparer<Kortti>
    {
        //Korttien vertailua

        public int Compare(Kortti? x, Kortti? y)
        {
            int results = x.Maa.CompareTo(y.Maa);
            if (results == 0)
                results = x.Arvo - y.Arvo;
            return results;
        }

        public static IComparer<Kortti> MaanMukaanJärjestys()
        {
            return new MaanMukaan();
        }
    }

    internal class KorttiPakka
    {
        //Lista kortteja varten:
        
        private List<Kortti> korttipakka;

        public KorttiPakka()
        {

            korttipakka = new List<Kortti>();

            string[] maat = { "Hertta", "Ruutu", "Risti", "Pata" };

            //Lisätään listaan jokerit
            
            for (int i = 0; i < 2; i++)
            {
                Kortti k = new Kortti(" ", 14);
                korttipakka.Add(k);
            }

            //Lisätään listaan loput kortit

            for (int i = 0; i < maat.Length; i++)
            {      
                for (int j = 1; j < 14; j++)
                {
                    Kortti k = new Kortti(maat[i], j);
                    korttipakka.Add(k);
                }
            }
        }

        //Metodi pakan sekoittamista varten
        public void Sekoita()
        {
            Random rand = new Random();

            int n = korttipakka.Count();
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                Kortti value = korttipakka[k];
                korttipakka[k] = korttipakka[n];
                korttipakka[n] = value;
            }
        }

        //Metodi kortin nostoa varten
        public Kortti Nosta()
        {
            Kortti paalimmainen = korttipakka[0];
            korttipakka.RemoveAt(0);
            return paalimmainen;
        }

        //Metodi korttien jakamista varten
        public Kasi[] Jaa(int pelaajienmaara)
        {
            //Luodaan taulukko Kasi-olioista
            Kasi[] kadet = new Kasi[pelaajienmaara];

            for (int i = 0; i < pelaajienmaara; i++)
            {
                //Luodaan Kasi-olio, johon nostetaan kortteja silmukassa
                Kasi k = new Kasi();
                for (int j = 0; j < 5; j++)
                {
                    Kortti s = Nosta();
                    k.Lisaa(s);
                }
                //lisätään olio taulukkoon
                kadet[i] = k;
            }
            //Palautetaan taulukko, jossa on pelaajien kädet
            return kadet;
        }

        //Metodi pakan järjestämistä varten
        public void JarjestaMaanMukaan()
        {
            korttipakka.Sort(MaanMukaan.MaanMukaanJärjestys());
        }

        //ToString-metodi
        public override string ToString()
        {
            string s = "";

            foreach (Kortti k in korttipakka)
            {
                s += k.ToString() +"\n";
            }

            return s;
        }
    }
}
