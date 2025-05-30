namespace EsportApp.Models
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Gname { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoBase64 { get; set; }
        public string Description { get; set; }
    }
}
