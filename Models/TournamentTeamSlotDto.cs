namespace EsportApp.Models
{
    public class TournamentTeamSlotDto
    {
        public int SlotNumber { get; set; }
        public int? TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamLogoBase64 { get; set; }
        public bool IsCurrentUserTeam { get; set; } 
    }
}
