using System.Collections.Generic;

namespace EsportApp.Models
{
    public class PlayerDetailsDto
    {
        public int Pid { get; set; }
        public string Pname { get; set; }
        public string Dob { get; set; }
        public string Sex { get; set; }
        public string Tname { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoBase64 { get; set; }
        public List<TeamShortDto> Teams { get; set; } = new();
        public List<TournamentShortDto> Tournaments { get; set; } = new();
    }
}
