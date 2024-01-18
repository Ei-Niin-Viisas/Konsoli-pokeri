namespace PokeriPeli
{
    internal class Pelaa
    {
        public KorttiPakka Pakka; //KorttiPakka-olio "Pakka"
        public Kasi[] kadet;      //Taulukko, johon pelaajien kädet laitetaan

        //Pelaa-olion konstruktori
        public Pelaa(int pelaajienmaara)
        {
            //Luodaan olion KorttiPakka ilmentymä "Pakka"
            Pakka = new KorttiPakka();
            Pakka.Sekoita();    //Sekoitetaan Pakka

            //Luodaan taulukko Kasi-oliosta Pakan Jaa-metodin avulla
            kadet = Pakka.Jaa(pelaajienmaara);

            //Näytetään pelaajan käsi pelaajalle ToString-metodin avulla
            Console.WriteLine("Sinun kätesi: ");
            Console.WriteLine(kadet[0].ToString());

            //Kerrotaan pelaajalle, miten hän voi vaihtaa kortteja
            Console.WriteLine("Mitkä kortit haluat vaihtaa " +
                "(anna järjestyslukuina 1-5)? ");

            Console.WriteLine("Voit vaihtaa enintään neljä korttia. " +
                "Erota kortit toisistaan pilkuilla ");

            Console.WriteLine("Jos et halua vaihtaa kortteja anna luku 0");

            //string-taulukko, johon pelaajan antama syöte "hajotetaan"
            string[] luvut;

            //int-taulukko, jossa on vaihdettavien korttien indeksit
            int[] numerot;
           
            //while-silmukka, joka katkeaa, jos pelaajan antama syöte kelpaa
            while (true) 
            {
                //Pelaajan antama syöte tekstinä
                string vaihdettavat = Console.ReadLine();
                
                //Katkaistaan teksti pilkkujen kohdalta taulukoksi
                luvut = vaihdettavat.Split(",");

                //Kutsutaan metodia Vaihto, joka tarkistaa, kelpaako syöte 
                //Jos syöte ei kelpaa, metodi asettaa taulukon ensimmäiseksi
                //alkoiksi luvun -1.
                //Jos söyte kelpaa, metodi palauttaa korttien indeksit
                numerot = Vaihto(luvut);    //sijoitetaan indeksit taulukkoon

                //Jos pelaaja ei halua vaihtaa kortteja, katkaistaan silmukka.
                if (vaihdettavat == "" | vaihdettavat == "0")
                {
                    break;
                }
                else if (numerot.Length > 4)
                //Jos taulukko numerot on pituudeltaan yli neljä, annetaan
                //virheilmoitus ja silmukka alkaa alusta.
                {
                    Console.WriteLine("Voit vaihtaa vain neljä korttia!");
                    Console.WriteLine("Anna uusi syöte");
                }
                //Jos metodi Vaihto on asettanut taulukon ensimmäiseksi arvoksi
                //luvun -1, annetaan virheilmoitus ja silmukka alkaa alusta.
                else if (numerot[0] == -1)
                {
                    Console.WriteLine("Virheellinen syöte");
                    Console.WriteLine("Yritä uudelleen ");
                }
                //Jos taulukko on hyväsytyssä muodossa, katkaistaan silmukka
                else
                {
                    //Kutsutaan luokan Kasi Vaihda-funktiota, jolle annetaan
                    //parametreinä KorttiPakka-olion ilmentymä sekä taulukko numerot,
                    //jossa on vaihdettavien korttien indeksit.
                    //Funktio nostaa pakasta uusia kortteja vaihdettavien tilalle.
                    kadet[0].Vaihda(Pakka, numerot);
                    break;
                }
            }
                     
            //Tulostetaan pelaajan uusi käsi
            Console.WriteLine("\nUusi kätesi: ");
            Console.WriteLine(kadet[0]);

            //Luodaan silmukassa olion TekoAly ilmentymä jokaista muuta pelaaja
            //kohti. TekoAly automaattisesti tarkastaa pelaajan käden ja
            //vaihtaa kortteja sen perusteella.
            for (int i = 1; i < kadet.Length; i++)
            {
                new TekoAly(kadet[i], Pakka);
            }
            
            //Tarkistetaan kaikki kädet silmukassa, jossa luodaan olion
            //Tarkastus ilmentymä jokaista kättä kohden. Tarkastettuaan käden
            //olio Tarkastus kutsuu oli Kasi funktiota, joka asettaa arvoja
            //attribuutteisiinsa. 
            foreach (Kasi k in kadet)
            {
                new Tarkastus(k);
            }

            //Luodaan olion Vertailu ilmentymä, jolle annetaan lista kadet
            //parametriksi. Olio vertailee käsiä toisiinsa ja asettaa
            //sijoitukset olion Kasi parametreihin
            new Vertailu(kadet);

            //Tulostetaan olion Pelaa ToString, joka näyttää kaikkien pelaajien
            //kädet ja sijoitukset.
            Console.WriteLine(ToString());
        }

        //Metodi Vaihto, joka määrittelee, onko pelaajan antama syöte
        //vaihdettavista korteista kelvollinen ja muuttaa syötteen korttien
        //indekseksi
        private int[] Vaihto(string[] syote)
        {
            //int taulukko paluuarvot, johon indeksit sijoitetaan
            int[] paluuarvot = new int[syote.Length];

            //Totuusarvot, joilla tarkistetaan, ettei sama luku ole annettu
            //kahdesti
            bool tupla1 = false;
            bool tupla2 = false;
            bool tupla3 = false;
            bool tupla4 = false;
            bool tupla5 = false;

            //Silmukka, joka käy läpi jokaisen parametrina annetun taulukon
            //alkion. Taulukon syote luvut 1-5 muutetaan luvuiksi 0-4
            //if-lauseen avulla. Saadut luvut asetetaan paluuarvot taulukkoon.
            //Jos taulukossa on jotain muuta kuin lukuja väliltä 1-5 asetaan
            //paluuarvot taulukon ensimmäiseksi alkioksi -1 ja katkaistaan
            //silmukka. Jokaisessa if- ja if else -lauseen haaran sisällä on
            //myös if-lause, joka tarkistaa, että samaa lukua ei ole annettu
            //kahdesti. Jos se on annettu kahdesti, asetaan paluuarvot[0] 
            //arvoon -1 ja katkaistaan silmukka.
            for (int i = 0; i < syote.Length; i++)
            {
                if (syote[i] == "1" | syote[i] == " 1")
                {
                    if (tupla1) { paluuarvot[0] = -1; break; }
                    
                    paluuarvot[i] = 0;
                    tupla1 = true;
                }
                else if (syote[i] == "2" | syote[i] == " 2")
                {
                    if (tupla2) { paluuarvot[0] = -1; break; }

                    paluuarvot[i] = 1;
                    tupla2 = true;
                }
                else if (syote[i] == "3" | syote[i] == " 3")
                {
                    if (tupla3) { paluuarvot[0] = -1; break; }
                    
                    paluuarvot[i] = 2;
                    tupla3 = true;
                }
                else if (syote[i] == "4" | syote[i] == " 4")
                {
                    if (tupla4) { paluuarvot[0] = -1; break; }

                    paluuarvot[i] = 3;
                    tupla4 = true;
                }
                else if (syote[i] == "5" | syote[i] == " 5")
                {
                    if (tupla5) { paluuarvot[0] = -1; break; }
                    
                    paluuarvot[i] = 4;
                    tupla5 = true;
                }
                else
                {
                    paluuarvot[0] = -1;
                    break;
                }
            }

            //Palautetaan taulukko paluuarvot
            return paluuarvot;
        }

        //ToString-metodi
        public override string ToString()
        {
            //luodaan taulukko taulukoista, joiden sisältö on olioiden Kasi
            //ToString katkaistuna osiin.
            string[][] tuloste = new string[kadet.Length][];

            //Luodaan string muuttuja kasi
            string kasi = "";
            //Luodaan string taulukko hajotettu
            string[] hajotettu;
            //Luodaan string muuttuja pelaajat, joka lopuksi palautetaan
            string pelaajat = "";

            //Lisätään silmukassa ensimmäiselle riville kaikki pelaajat
            for (int i = 1; i < kadet.Length + 1; i++)
            {
                pelaajat += $"Pelaaja {i} \t";
            }

            //Lisätään rivivaihto
            pelaajat += "\n";

            //Hajotetaan jokaisen olion Kasi ToString metodin paluuarvo
            //rivivaihtojen kohdalta ja lisätään ne taulukkoon tuloste
            for (int i = 0; i < kadet.Length; i++)
            {
                kasi = kadet[i].ToString();
                hajotettu = kasi.Split("\n");
                tuloste[i] = hajotettu;
            }

            //Sisäkkäinen silmukkarakenne, joka muotoilee muuttujaa pelaajat
            //siten, että se näyttää samalta kuin olioiden Kasi ToString-
            //metodien syöte näyttäisi vierekkäin.
            for (int i = 0; i < tuloste[0].Length; i++)
            {
                string h = "";

                for (int j = 0; j < kadet.Length; j++)
                {
                    h += tuloste[j][i] + "  \t";
                }

                pelaajat += h + "\n";
            }

            //Palautetaan muuttuja pelaajat
            return pelaajat;
        }

    }
}
