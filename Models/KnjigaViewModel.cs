namespace BiblioMeta2.Models
{
    public class KnjigaViewModel
    {
        public int KnjigaID { get; set; }
        public string Naslov { get; set; }
        public int StZnakov { get; set; }
        public int StStrani { get; set; }
        public float Cena { get; set; }
        public int ZanrID { get; set; }
        public string AvtorIme { get; set; } 
        public string Zanr { get; set;}
    }
}