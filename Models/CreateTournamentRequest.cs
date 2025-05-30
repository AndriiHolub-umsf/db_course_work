namespace EsportApp.Models
{
    public class CreateTournamentRequest
    {
        public int Gid { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int MaxTeams { get; set; }
        public int PointsToWin { get; set; }
        public int PlayersInTeam { get; set; }
    }
}
