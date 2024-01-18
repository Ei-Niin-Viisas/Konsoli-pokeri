namespace PokeriPeli
{
    internal class Tarkastus
    {
        //Luodaan luokan attribuutit Kasi, Arvot, Maat ja Jokerit
        Kasi Kasi;              //Sisältää tarkastettavan Kasi-olion
        List<int> Arvot;        //Sisältää korttien numero-arvot
        List<string> Maat;      //Sisältää korttien maat
        List<int> Jokerit;      //Sisältää jokerit

        //Luokan konstruktori, jossa attribuutteihin sijoitetaan arvot ja,
        //josta luokan metodeja kutsutaan
        public Tarkastus(Kasi kasi)
        {
            Kasi = kasi;
            Arvot = new List<int>();
            Maat = new List<string>();
            Jokerit = new List<int>();

            //Lisätään listoihin niihin kuuluvat arvot
            for (int i = 0; i < Kasi.Kortit.Length; i++)
            {
                if (Kasi.Kortit[i].Arvo == 14)
                {
                    Jokerit.Add(14);
                }
                else
                {
                    Arvot.Add(Kasi.Kortit[i].Arvo);
                    Maat.Add(Kasi.Kortit[i].Maa);
                }
            }

            //Kutsutaan metodia Lopputulos, joka asettaa Kasi-olioon oikeat
            //arvot
            Lopputulos();
        }

        //Metodi, joka tarkistaa, onko kädessä pareja, kolme samaa... jne.
        private int[] TarkistaSamat()
        {
            //Luodaan laskureita, jotka pitävät tallessa, onko kädessä samoja
            //kortteja ja parin tapauksessa muistaa, onko kädessä yksi vai
            //kaksi paria
            int parit = 0;
            int kolmoset = 0;
            int neloset = 0;
            int vitoset = 0;

            //Luodaan muuttujia, jotka pitävät tallessa korttien arvon, jos
            //kädessä on samoja kortteja.
            //Parin tapauksessa täytyy luoda taulukko.
            int[] parinarvo = new int[2];
            int kolmostenarvo = 0;
            int nelostenarvo = 0;
            int vitostenarvo = 0;

            //Muuttuja laskuri, joka kuvaa kuinka monta samaa luku kädessä on
            int laskuri = 1;

            //Järjestetään lista Arvot, jotta seuraa silmukka toimisi oikein
            Arvot.Sort();

            //Luodaan for-silmukka, joka käy kaikki listan uniikit arvot läpi.
            //Jos listassa on samoja arvoja, se selvitetään while-silmukassa.
            for (int i = 0; i < Arvot.Count - 1 ; i++)
            {
                //for-silmukan sisäinen while-silmukka tarkistaa, onko seuraa
                //luku sama kuin luku, jonka indeksi on i. Jos on, kasvatetaan
                //i:tä ja laskuria yhdellä ja while-silmukka alkaa alusta. Jos
                //luku ei ole sama tai i kasvaa liian isoksi, katkaistaan
                //katkaistaan silmukka.
                while (true)
                {
                    if (i == Arvot.Count - 1) break;
                    else if (Arvot[i] == Arvot[i + 1])
                    {
                        laskuri++;
                        i++;
                    }
                    else break;
                }
            
                //If-lause, joka selvittää, onko laskuria kasvatettu while-
                //silmukassa. Laskurin luku kertoo, montako samaa korttia
                //kädessä on. Numeron arvo tallennetaan muuttujaan ja toista
                //muuttujaa kasvatetaan yhdellä, jotta myöhemmin tiedetään,
                //montako samaa kädessä oli.

                //Pari täytyy käsitellä eri tavalla kuin muut,
                //koska kädessä voi olla kaksi paria. 
                if (laskuri == 2)
                {
                    //Jos kysessä on ensimmäinen pari, tallennetaan sen arvo
                    //taulukon ensimmäiseen indeksiin. 
                    if (parit == 0)
                    {
                        parinarvo[0] = Arvot[i];
                    }
                    //Jos kyseessä on toinen pari, tarkistetaan kummalla on
                    //isompi arvo ja asetetaan se indeksiin 0. Pienemmän parin
                    //arvo asetetaan indeksiin 1.
                    else
                    {
                        if (parinarvo[0] < Arvot[i])
                        {
                            parinarvo[1] = parinarvo[0];
                            parinarvo[0] = Arvot[i];
                        }
                        else
                        {
                            parinarvo[1] = Arvot[i];
                        }
                    }
                    //Kasvatetaan muuttujaa parit
                    parit++;
                }
                else if (laskuri == 3) 
                { 
                    kolmoset++; kolmostenarvo = Arvot[i]; 
                }
                else if (laskuri == 4) 
                { 
                    neloset++; nelostenarvo = Arvot[i]; 
                }
                else if (laskuri == 5) 
                { 
                    vitoset++; vitostenarvo = Arvot[i]; 
                }

                //Palautetaan laskuri arvoon 1
                laskuri = 1;
            }

            //If lause, joka tarkistaa, onko kädessä jokereita
            //ja tarvittaessa kasvattaa esim. parin, kolmeksi samaksi
            //sekä tekee tarvittavat muutokset laskureihin
            if (Jokerit.Count() != 0)
            {
                //Tarkistetaan, mihin jokeri kuuluu
                for (int i = 0; i < Jokerit.Count(); i++)
                {
                    if (neloset == 1)
                    {
                        vitoset = 1;
                        neloset = 0;
                        vitostenarvo = nelostenarvo;
                    }
                    else if (kolmoset == 1)
                    {
                        neloset = 1;
                        kolmoset = 0;
                        nelostenarvo = kolmostenarvo;
                    }
                    else if (parit == 1)
                    {
                        kolmoset = 1;
                        parit = 0;
                        kolmostenarvo = parinarvo[0];
                    }
                    else if (parit == 2)
                    {
                        kolmoset = 1;
                        parit = 1;
                        kolmostenarvo = parinarvo[0];
                        parinarvo[0] = parinarvo[1];
                    }
                    //Jos kädessä ei ole mitään samoja kortteja,
                    //jokeri muodostaa parin suurimman kortin kanssa
                    //(erillinen tarkistus ässälle)
                    else
                    {
                        parit = 1;
                        if (Arvot.Contains(1))
                        {
                            parinarvo[0] = 1;
                        }
                        else
                        {
                            parinarvo[0] = Arvot.Max();
                        }
                    }
                } 
            }

            //Luodaan taulukko paluuarvot ja sijoitetaan oikeat arvot siihen.
            //Indeksiin 0 menee, montako samaa korttia kädessä oli.
            //Indeksiin 1 menee, mikä oli korttien lukuarvo.
            //Indeksiin 2 menee parin ja täydenkäden tapauksessa heikompi
            //lukuarvo. Muissa tapauksissa sillä on arvo -1.
            //Indeksiin 3 menee, montako "turhaa" korttia kädessä on.
            int[] paluuarvot = new int[4];
            paluuarvot[2] = -1;

            if (vitoset == 1)
            {
                paluuarvot[0] = 5;
                paluuarvot[1] = vitostenarvo;
                paluuarvot[3] = 0;
            }
            else if (neloset == 1)
            {
                paluuarvot[0] = 4;
                paluuarvot[1] = nelostenarvo;
                paluuarvot[3] = 1;
            }
            else if (kolmoset == 1 && parit == 1)
            {
                paluuarvot[0] = 32;
                paluuarvot[1] = kolmostenarvo;
                paluuarvot[2] = parinarvo[0];
                paluuarvot[3] = 0;

            }
            else if (kolmoset == 1)
            {
                paluuarvot[0] = 3;
                paluuarvot[1] = kolmostenarvo;
                paluuarvot[3] = 2;
            }
            else if (parit == 2)
            {
                paluuarvot[0] = 22;
                paluuarvot[1] = parinarvo[0];
                paluuarvot[2] = parinarvo[1];
                paluuarvot[3] = 1;
            }
            else if (parit == 1)
            {
                paluuarvot[0] = 2;
                paluuarvot[1] = parinarvo[0];
                paluuarvot[3] = 3;
            }
            else
            {
                //Jos kädessä ei ole yhtään samoja kortteja, asetetaan
                //indeksiin 1 suurimman kortin arvo.
                //Erillinen tarkastelu ässälle.
                paluuarvot[0] = 1;
                if (Arvot.Contains(1))
                {
                    paluuarvot[1] = 1;
                }
                else
                {
                    paluuarvot[1] = Arvot.Max();
                }
                paluuarvot[3] = 4;
            }

            //Palautetaan taulukko paluuarvot

            return paluuarvot;

        }


        private string[] TarkistaVari()
        {
            string[] paluuarvot = new string[2];    //Taulukko paluuarvoille

            //Silmukka, jossa tarkistetaan, onko kaikilla korteilla sama maa
            for (int i = 0; i < Maat.Count-1; i++)
            {
                //If lause, joka tarkistaa, onko peräkkäisillä korteilla sama
                //maa.
                if (Maat[i] == Maat[i + 1])
                {
                    //If lause, joka aktivoituu, jos i + 1 on taulukon
                    //viimeinen indeksi. Kädessä on silloin väri.
                    if (i == Maat.Count-2)
                    {
                        paluuarvot[0] = "Väri";
                        paluuarvot[1] = Maat[0];
                    }
                }
                //Jos peräkkäiset kortit eivät ole samaa maata, katkaistaan
                //silmukka ja todetaan, että kädessä ei ole väriä.
                else
                {
                    paluuarvot[0] = "Ei väriä";
                    break;
                }
            }

            return paluuarvot; //Palautetaan arvot
        }

        private string[] TarkistaSuora()
        {
            //Paluuarvot
            string[] paluuarvot = new string[2];

            //Laskuri jokerien määrästä, jota tarvitaan, jos jokeri sijoittuu
            //suoran sisälle. (esim. kahden ja neljän väliin)
            int pitycounter = Jokerit.Count;

            //Järjestetään käsi
            Arvot.Sort();
            //Muuttuja assa, joka muistaa, onko ässä muutettu arvoon 14
            bool assa = false;
            

            //Tarkistetaan silmukassa ovatko arvot peräkkäisiä
            for (int i = 0; i < Arvot.Count-1; i++)
            {
                if (Arvot[i] + 1 == Arvot[i + 1])   //Jos peräkkäisiä
                {
                }
                //Jos välistä puuttuu yksi, mutta kädessä on jokeri
                else if (Arvot[i] + 2 == Arvot[i + 1] && pitycounter > 0)
                {
                    pitycounter--;
                }
                //Jos välistä puuttuu 2, mutta kädessä on 2 jokeria
                else if (Arvot[i] + 3 == Arvot[i + 1] && pitycounter > 1)
                {
                    pitycounter -= 2;
                }
                //Jos ensimmäinen kortti on ässä ja for-silmukka on vasta 
                //ensimmäisillä kierroksella
                else if (i == 0 && Arvot[0] == 1)
                {
                    //Muutetaan ässä luvuksi 14, jos se vaikka olisikin suoran
                    //suurin kortti
                    
                    Arvot.RemoveAt(0);  //Poistetaan ässä
                    Arvot.Add(14);      //Lisätään 14
                    assa = true;        //Muutataan assa arvoon true

                    //Pienennetään i:tä, jotta for silmukka tarkistaa
                    //peräkkäisyyden oikein
                    i--;
                }
                //Jos mikään edellisistä ei täyty, todetaan, että ei suoraa ja
                //katkaistaan silmukka
                else
                {
                    paluuarvot[0] = "Ei suoraa";
                    break;
                }

                //Jos silmukka saa pyöritää kaikki listan alkiot katkeamatta
                //läpi, todetaan, että kädessä on suora
                if (i == Arvot.Count - 2)
                {
                    paluuarvot[0] = "Suora";

                    //If lause, joka asettaa suoran suurimman luvun
                    //paluuarvojen indeksiin 1
                    if (Arvot[0] >= 10)
                    {
                        paluuarvot[1] = "14";
                    }
                    else
                    {
                        paluuarvot[1] = $"{Arvot[0] + 4}";
                    }
                }
            }

            //Jos ässää tarkasteltiin suurimpana arvona,
            //palautetaan se arvoon yksi
            if (assa)
            {
                //While-silmukka siltä varalata, että useampi kuin yksi ässä
                //muutettiin arvoon 14
                while (true)
                {
                    Arvot.Remove(14);
                    Arvot.Add(1);
                    if (Arvot.Contains(14)) continue;
                    else break;
                }
            }

            return paluuarvot;  //Palautetaan arvot
        }

        public void Lopputulos()
        {
            //Kutsutaan metodia TarkistaSamat, joka tarkistaa, montako samaa
            //korttia kädessä on. Tiedot tallennetaan int taulukkoon samat.
            int[] samat = TarkistaSamat();

            //Kutsutaan metodia TarkistaVari, joka tarkistaa, onko kädessä
            //väriä. Tiedot tallennetaan string taulukkoon vari.
            string[] vari = TarkistaVari();

            //Kutsutaan metodia TarkistaSuora, joka tarkistaa, onko kädessä
            //suoraa. Tiedot tallennetaan string taulukkoon suora.
            string[] suora = TarkistaSuora();

            //Muuttuja kuvio, johon tallennetaan parhaan kädessä olevan kuvion
            //nimi. Esim. täyskäsi tai pari.
            string kuvio;

            //Taulukko paluuarvot, johon tallennetaan kuvion arvo. Esim. parin
            //kohdalla tallentaan, minkä luvun pari on kyseessä.
            int[]? paluuarvot = null;

            //Taulukko hait, johon tallennetaan kuvion ulkopuoliset luvut
            //Tarvitaan voittajan ratkaisussa, jos voittajilla on sama kuvio.
            int[]? hait = null;

            //Tarkistetaan, onko kädessä värisuora, ja, jos on, asetetaan sen
            //mukaiset tiedot muuttujaan kuvio ja taulukkoon paluuarvot.
            if (suora[0] == "Suora" && vari[0] == "Väri")
            {
                kuvio = "Värisuora";
                paluuarvot = new int[1];
                paluuarvot[0] = int.Parse(suora[1]);
            }
            //Tarkistetaan, onko kädessä väri ja asetetaan sen
            //mukaiset tiedot muuttujaan kuvio ja taulukkoon hait.
            else if (vari[0] == "Väri")
            {
                kuvio = "Väri   ";

                //Muuttuja indeksi, jota tarvitaan, kun sijoitetaan lukuja
                //taulukkoon hait.
                int indeksi = 0;

                //Jos useammalla pelaajalla on, väri suurin kortti ratkaisee
                //Asetetaan siis kaikkien korttien arvot taulukkoon hait
                hait = new int[5];

                //Asetetaan jokerit taulukkoon
                foreach (int jokeri in Jokerit)
                {
                    hait[indeksi] = jokeri;
                    indeksi++;
                }

                //Asetetaan muut luvut taulukkoon
                foreach (int numero in Arvot)
                {
                    hait[indeksi] = numero;
                    indeksi++;
                }
            }
            //Tarkistetaan, onko kädessä suora, ja asetetaan sen mukaiset
            //tiedot muuttujaan kuvio ja taulukkoon paluuarvot.
            else if (suora[0] == "Suora")
            {
                kuvio = "Suora";
                paluuarvot = new int[1];
                paluuarvot[0] = int.Parse(suora[1]);
            }
            //Tarkistetaan, montako samaa kädessä on, jos yhtään.
            else
            {
                //Taulukko hait, jonka pituus määrittyy "turhien" korttien
                //mukaan
                hait = new int[samat[3]];

                //muuttuja indeksi, jota tarvitaan, kun lukuja sijoitetaan
                //taulukkon hait
                int indeksi = 0;

                //Asetetaan muuttujaan kuvio oikeat tiedot sen mukaa, montako
                //samaa kädessä oli. Jos käsi on täynnä, asetetaan taulukko
                //hait arvoon null.
                if (samat[0] == 5) { kuvio = "Viisi samaa"; hait = null; }
                else if (samat[0] == 4) { kuvio = "Neljä samaa"; }
                else if (samat[0] == 32) { kuvio = "Täyskäsi"; hait = null; }
                else if (samat[0] == 3) { kuvio = "Kolme samaa"; }
                else if (samat[0] == 22) { kuvio = "Kaksi paria"; }
                else if (samat[0] == 2) { kuvio = "Pari   "; }
                else { kuvio = "Hai   "; }
                
                //Asetetaan taulukkoon paluuarvot oikeat tiedot.
                //Kahden parin ja täyskäden tapauksessa taulukkoon tulee kaksi
                //tietoa. Muissa tapauksissa vain yksi.
                if (samat[0] == 32 | samat[0] == 22)
                {
                    paluuarvot = new int[] { samat[1], samat[2] };
                }
                else
                {
                    paluuarvot = new int[] { samat[1] };
                }

                //Jos taulukkoa hait ei ole astettu arvoon null, sijoitetaan
                //sinne oikeat arvot.
                if (hait != null)
                {
                    foreach (int luku in Arvot)
                    {
                        if (luku != samat[1] && luku != samat[2])
                        {
                            hait[indeksi] = luku;
                            indeksi++;
                        }
                    }
                }
            }

            //Kutsutaan olion Kasi metodia aseta arvot, joka asettaa oikeat
            //tiedot olion attribuutteihin
            Kasi.AsetaArvot(kuvio, paluuarvot, hait);
        }
    }
}
