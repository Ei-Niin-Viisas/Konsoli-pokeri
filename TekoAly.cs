namespace PokeriPeli
{
    internal class TekoAly
    {
        //Luokan attribuutti kasi
        Kasi kasi;

        //Lista, jossa on vaihdettavien korttien indeksit
        List<int> vaihdot;

        //Taulukko, johon listassa vaihdot olevat indeksit lopuksi sijoitetaan,
        //koska olion Kasi metodi Vaihto haluaa indeksit taulukossa
        int[] indeksit;

        public TekoAly(Kasi parametri, KorttiPakka pakka)
        {
            //Sijoitetaan käsi attribuuttiin
            kasi = parametri;

            //Luodaan uusi lista vaihdot
            vaihdot = new List<int>();

            //Luodaan olion Tarkastus ilmentymä, joka tarkastaa käden ja
            //asettaa sen tiedot olion Kasi attribuutteihin
            new Tarkastus(kasi);

            //muuttuja assa, jota tarvitaan ässän tarkastelussa
            bool assa = false;
            if (kasi.KuvionArvo1 == 14 | kasi.KuvionArvo2 == 14)
            {
                assa = true;
            }

            //Jos VertLuku on suurempi kuin 8, ei tehdä mitään.
            //(Silloin kädessä on joko viisi samaa tai värisuora)
            if (kasi.Vertluku > 8)
            {
                
            }
            //Jos VerLuku on pienempi kuin 8, mutta suurempi kuin 4, ei tehdä
            //mitään. (Silloin kädessä on joko suora, väri, tai täyskäsi)
            else if (kasi.Vertluku < 8 && kasi.Vertluku > 4)
            {

            }
            //Muissa tapauksissa vaihdetaan kortteja
            else
            {
                //Silmukka, joka käy kaikki olion Kasi kortit läpi
                for (int i = 0; i < kasi.Kortit.Length; i++)
                {
                    //Tarkstus, jotta jokereita ei vaihdeta
                    if (kasi.Kortit[i].Arvo == 14)
                    {
                        
                    }
                    //Tarkastus, jotta kortteja, joita on kädessä useampia ei
                    //vaihdeta pois
                    else if (kasi.Kortit[i].Arvo == kasi.KuvionArvo1 |
                        kasi.Kortit[i].Arvo == kasi.KuvionArvo2)
                    {

                    }
                    //Sama tarkistus kuin ylempänä, mutta ässälle
                    else if(assa && kasi.Kortit[i].Arvo == 1)
                    {

                    }
                    //Muut kortit vaihdetaan eli lisätään kortin indeksi 
                    //listaan vaihdot
                    else
                    {
                        vaihdot.Add(i);
                    }
                }
            }

            //Luodaan taulukko, joka on yhtä pitkä kuin lista
            indeksit = new int[vaihdot.Count];

            //Sijoitetaan listassa olevat indeksit taulukkoon
            for (int i = 0; i < vaihdot.Count; i++)
            {
                indeksit[i] = vaihdot[i];
            }

            //Kutsutaan olin Kasi Vaihda-metodia. 
            //Metodi vaihtaa indeksien mukaiset kortit
            kasi.Vaihda(pakka, indeksit);
        }
    }
}
