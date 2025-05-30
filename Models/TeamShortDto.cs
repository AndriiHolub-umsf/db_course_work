namespace EsportApp.Models
{
    public class MyTeamShortDto
    {
        public int Id { get; set; }
        public string Tname { get; set; }
    }
    public class TeamShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Сaptain_id { get; set; }

        public string PhotoBase64 { get; set; }
    }
}
