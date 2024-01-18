namespace PokeriPeli
{
    internal class Program
    {

        //Main-funktio
        static void Main(string[] args)
        {
            //While-silmukka, jossa on virhekäsittely sen varalta,
            //että pelaajien määrä annetaan virheellisesti

            //(Käsittely tosin nappaa kaikki virheet, mutta ei niitä
            //pitäisi muualta tulla.)
            while (true)
            {
                try
                {
                    Console.WriteLine("Montako pelaajaa on mukana (max 5)? ");
                    int pelaajat = int.Parse(Console.ReadLine());

                    //If-lause tutkii on luku sopivalta väliltä
                    if (pelaajat > 5 | pelaajat < 2)
                    {
                        Console.WriteLine("Pelaajia voi olla 2 - 5.");
                    }
                    else
                    {
                        //Jos pelaajia on sopiva määrä, luodaan Pelaa-olion
                        //ilmentymä, jonka parametrinä on pelaajien määrä

                        new Pelaa(pelaajat);
                        break; //Katkaistaan silmukka
                    }
                }
                catch
                {
                    //Virheilmoitus
                    Console.WriteLine("Virheellinen syöte");
                }
            }
        }
    }
}