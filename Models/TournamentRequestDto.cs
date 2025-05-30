namespace EsportApp.Models
{
    public class TournamentRequestDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Status { get; set; }
        public string CaptainName { get; set; }
        public string CaptainEmail { get; set; }
    }

    public class TournamentMyRequestDto
    {
        public int? TeamId { get; set; }
        public string Status { get; set; }
    }

    public class TournamentInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GameName { get; set; }
        public string GamePhotoBase64 { get; set; }
        public DateTime Date { get; set; }
        public int MaxTeams { get; set; }
        public int PointsToWin { get; set; }
        public int PlayersInTeam { get; set; }
        public int ApprovedCount { get; set; }
        public bool Started { get; set; }
        public List<TeamShortDto> Teams { get; set; }
        public List<MatchDto> Matches { get; set; }
        public List<TournamentRequestDto> Requests { get; set; } 
        public TournamentMyRequestDto MyRequest { get; set; }

        public int? WinnerId { get; set; }
        public string WinnerName { get; set; }
    }
}
