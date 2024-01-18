namespace PokeriPeli
{
    internal class Kasi
    {
        //Taulukko Kortit, johon Kortti-oliot tallennetaan
        public Kortti[] Kortit { get; private set; }

        //Muuttuja Kuvio, johon tallennetaan, mikä käsi pelaajalla on
        //esim. Pari tai Väri
        public string Kuvio { get; private set; }

        //Muuttuja johon tallennetaan, mitä luku käden kuvio edustaa
        //Esim. jätkä parissa tähän tallennetaan luku 11
        public int KuvionArvo1 { get; private set; }

        //Muuttuja, johon tallennetaan kahden parin ja täyskäden tapauksessa
        //se heikompi arvo. 
        public int KuvionArvo2 { get; private set; }

        //Taulukko Hait, jossa käden kuvion ulkopuoliset luvut
        public int[] Hait { get; private set; }

        //Muuttuja VertLuku, johon tallennetaan lukuarvo sen perusteella,
        //kuinka vahva kuvio pelaajalla on. Viisi samaa saa arvon 10 ja Hai
        //saa arvon 1
        public int Vertluku { get; private set; }

        //Muuttuja Sijoitus, johon tallennetaan pelaajan sijoitus.
        public int Sijoitus { get; private set; }


        //Luokan konstruktori, joka luo tyhjän taulukon kortteja varten
        public Kasi()
        {
            Kortit = new Kortti[5];
        }

        //Metodi, jolle annetaan kortti parametrinä. Metodi asettaa kortin
        //taulukkoon oikeaan eli ensimmäiseen tyhjään indeksiin.
        public void Lisaa(Kortti kortti)
        {
            for (int i = 0; i < Kortit.Length; i++)
            {
                if (Kortit[i] == null)
                {
                    Kortit[i] = kortti;
                    break;
                }
            }
        }

        //Metodi, jolle annetaan KorttiPakka, josta kortit nostetaan sekä
        //vaihdettavien korttien indeksit. 
        public void Vaihda(KorttiPakka pakka, int[] indeksit)
        {
            for (int i = 0; i < indeksit.Length; i++)
            {
                //Nostetaan kortti pakan Nosta-metodilla
                Kortti nosto = pakka.Nosta();
                
                //Korvataan indeksin mukainen kortti nostetulla kortilla
                Kortit[indeksit[i]] = nosto;
            }
        }


        //Metodi, joka asettaaa arvot olion attribuuteille: Kuvio, KuvionArvo1,
        //KuvionArvo2, Hait ja VertLuku
        //Metodi saa asettavat tiedot parametreina
        public void AsetaArvot(string muoto, int[] muodonluvut, int[] luvut)
        {
            //Asetetaan Kuvio
            Kuvio = muoto;

            //Tarkastetaan, onko muodonluvut null
            if (muodonluvut != null)
            {
                //Asetetaan KuvionArvot,
                //(erillinen tarkastelu ässälle)

                //Jos taulukon pituus on kaksi (esim. täyskäsi)
                if (muodonluvut.Length == 2)
                {
                    if (muodonluvut[0] == 1) KuvionArvo1 = 14;
                    else KuvionArvo1 = muodonluvut[0];

                    if (muodonluvut[1] == 1) KuvionArvo2 = 14;
                    else KuvionArvo2 = muodonluvut[1];
                }
                //Jos taulukon pituus on yksi (esim. suora)
                else if (muodonluvut.Length == 1)
                {
                    if (muodonluvut[0] == 1) KuvionArvo1 = 14;
                    else KuvionArvo1 = muodonluvut[0];
                }
            }

            //Tarkistetaan, onko taulukko luvut null
            if (luvut != null)
            {
                //Luodaan taulukko Hait, joka on yhtä pitkä kuin taulukko luvut
                Hait = new int[luvut.Length];

                //Sijoitetaan taulukon luvut arvot taulukkoon Hait
                //Luku 1 eli ässä muutetaan arvoon 14
                for (int i = 0; i < luvut.Length; i++)
                {
                    if (luvut[i] == 1)
                    {
                        Hait[i] = 14;
                    }
                    else
                    {
                        Hait[i] = luvut[i];
                    }
                }

                //Järjestetään taulukko Hait ja käännetään se, jotta luvut
                //olisivat järjestyksessä suurimmasta pienempään
                Array.Sort(Hait);
                Array.Reverse(Hait);
            }

            //Asetetaan oliolle Kuvion mukainen Vertluku
            if (Kuvio == "Viisi samaa") { Vertluku = 10; }
            else if (Kuvio == "Värisuora") { Vertluku = 9; }
            else if (Kuvio == "Neljä samaa") { Vertluku = 8; }
            else if (Kuvio == "Väri   ") { Vertluku = 7; }
            else if (Kuvio == "Suora") { Vertluku = 6; }
            else if (Kuvio == "Täyskäsi") { Vertluku = 5; }
            else if (Kuvio == "Kolme samaa") { Vertluku = 4; }
            else if (Kuvio == "Kaksi paria") { Vertluku = 3; }
            else if (Kuvio == "Pari   ") { Vertluku = 2; }
            else { Vertluku = 1; }
            
        }

        //Metodi, jolle annetaan pelaajan sijoitus parametrina ja joka asettaa
        //sen attribuuttiin Sijoitus
        public void MaaritaSijoitus(int luku)
        {
            Sijoitus = luku;
        }

        //Olion Kasi ToString-metodi.
        //Se näyttää pelaajan sijoituksen, käden ja kortit
        public override string ToString()
        {
            string s = " \n";
            if (Sijoitus != 0)
            {
                s += "Sijoitus: " + Sijoitus + " \n";
            }

            if (Kuvio != null)
            {
                s += "Kätesi: \n" + Kuvio + " \n\n";
            }

            foreach (Kortti kortti in Kortit)
            {
                s += kortti.ToString() + "\n";
            }
            return s;
        }
    }
}
