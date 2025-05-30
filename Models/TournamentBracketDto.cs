using System;
using System.Collections.Generic;

namespace EsportApp.Models
{
    public class TournamentBracketDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxTeams { get; set; }
        public int PointsToWin { get; set; }
        public DateTime Date { get; set; }
        public bool Started { get; set; }
        public List<BracketRoundDto> Bracket { get; set; }
    }
}
