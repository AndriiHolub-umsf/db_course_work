namespace EsportApp.Models
{
    public class TeamDetailsDto
    {
        public string Tname { get; set; }
        public int Id { get; set; }
        public int Captain_id { get; set; }
        public string Captain_Name { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoBase64 { get; set; }
    }
}
