namespace EsportApp.Models
{
    public class TournamentTeamDto
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int TeamId { get; set; }
        public string Status { get; set; }
    }
}
