namespace PokeriPeli
{
    internal class Vertailu
    {
        //Taulukko Kadet, johon sijoitetaan vertailtavat Kasi-oliot
        Kasi[] Kadet;
        //Taulukko sijoitukset, johon tallennetaan pelaajien sijoitukset
        int[] sijoitukset;

        //Luokan konstruktori, jolle annetaan vertailtavat kädet parametrina
        public Vertailu(Kasi[] pelaajat)
        {
            //Asetetaan pelaajat attribuuttiin Kadet
            Kadet = pelaajat;
            //Luodaan uusi taulukko sijoitukset, joka on yhtä pitkä kuin Kadet
            sijoitukset = new int[pelaajat.Length];

            //Sisäkkäinen silmukkarakenne, joka vertaa jokaista kättä kaikkiin
            //käsiin. Jos käsi on pienempi kuin käsi, johon sitä verrataan
            //käden kanssa samassa indeksissä oleva sijoituksen arvo kasvaa
            //yhdelllä.

            //Ulompi silmukka, joka käy kaikki kortit läpi.
            for (int i = 0; i < pelaajat.Length; i++)
            {
                //Asetataan sijoitus[i] arvoon yksi. Suurimman käden kohdalla
                //taulukon arvo pysyy arvossa yksi.
                sijoitukset[i] = 1;

                //Sisempi silmukka, joka vertaa ulomman silmukan korttia
                //kaikkiin kortteihin
                for (int j = 0; j < pelaajat.Length; j++)
                {
                    //Metodi Vertaa, joka palauttaa arvon true, jos Kadet[j] on
                    //suurempi kuin Kädet[i].
                    if (Vertaa(Kadet[i], Kadet[j]))
                    {
                        //Jos Kadet[i] on pienempi, sijoitukset[i] kasvaa
                        //yhdellä
                        sijoitukset[i]++;
                    }
                }

                //Kutsutaan olion Kasi metodia MaaritaSijoitus, joka tallentaa
                //sijoituksen olioon.
                Kadet[i].MaaritaSijoitus(sijoitukset[i]);
            }

        }

        //Metodi vertaa, joka palauttaa arvon true, jos "eka" on pienmepi kuin
        //"toka"
        private bool Vertaa(Kasi eka, Kasi toka)
        {
            //Ensin verrataan VerLukuja
            if (eka.Vertluku > toka.Vertluku) { return false; }
            else if (eka.Vertluku < toka.Vertluku) { return true; }
            
            //Sitten KuvionArvoja
            else if (eka.KuvionArvo1 > toka.KuvionArvo1) { return false; }
            else if (eka.KuvionArvo1 < toka.KuvionArvo1) { return true; }
            else if (eka.KuvionArvo2 > toka.KuvionArvo2) { return false; }
            else if (eka.KuvionArvo2 < toka.KuvionArvo2) { return true; }
            
            //Lopuksi kuvion ulkopuolisia kortteja, jotka ovat taulukossa Hait
            else
            {
                if (eka.Hait != null)
                {
                    for (int i = 0; i < eka.Hait.Length; i++)
                    {
                        if (eka.Hait[i] > toka.Hait[i]) { return false; }
                        else if (eka.Hait[i] < toka.Hait[i]) { return true; }
                    }
                }

                //Jos kädet ovat yhtä suuria palautetaan false
                return false;   
            }
        }


    }
}
