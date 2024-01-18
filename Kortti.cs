namespace PokeriPeli
{
    internal class Kortti : IComparable<Kortti>
    {
        //Olion Kortti attribuutti Maa
        public string Maa { get; private set; }
        
        //Olion Kortti attribuutti Arvo
        public int Arvo { get; private set; }
        
        //Luokan konstruktori 
        public Kortti(string maa, int arvo)
        {
            //Jos kortin arvo on 14 tehään siitä jokeri
            if (arvo == 14) { this.Maa = "jokeri"; }
            //Muissa tapauksissa kortin maa ja arvo annetaan parametreinä
            else { Maa = maa; }
            Arvo = arvo; 
        }

        //ToString-metodi, joka vaihtaa nimettyjen korttien kohdalla arvon
        //tilalle sen nimen.
        public override string ToString()
        {
            string s = this.Maa + " ";
            switch(this.Arvo)
            {
                case 1:
                    s += "ässä";
                    break;
                case 11:
                    s += "jätkä";
                    break ;
                case 12:
                    s += "rouva";
                    break;
                case 13:
                    s += "kurko";
                    break;
                case 14:
                    s += "jokeri";
                    break;
                default:
                    s += this.Arvo.ToString();
                    break;

            }
            return s;
        }
        //Vertoiluun vaadittava metodi (tehtiin tunnilla)
        public int CompareTo(Kortti other)
        {
            int result = this.Arvo - other.Arvo;

            if (result == 0)
            {
                result = this.Maa.CompareTo(other.Maa);
            }

            return result;
        }
    }
}
