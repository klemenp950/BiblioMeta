namespace BiblioMeta.Models
{
    public class Knjiga{
        public int KnjigaID {get; set;}
        public required String Naslov{get; set;}
        public int StStrani {get; set;}
        public int StZnakov {get; set;}
        public float Cena {get; set;}
        public int ZanrID {get; set;}
        public int AvtorID { get; set; }
    }
}