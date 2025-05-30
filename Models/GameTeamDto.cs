namespace EsportApp.Models
{
    public class GameTeamDto
    {
        public string Team_Name { get; set; }
        public string Captain_Name { get; set; }
        public byte[] Team_Photo { get; set; }
        public string Team_PhotoBase64 { get; set; }
    }
}
