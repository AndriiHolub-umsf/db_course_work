using System;
using System.Collections.Generic;

namespace EsportApp.Models
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int TeamCount { get; set; }
        public List<TeamShortDto> Teams { get; set; }
        public List<MatchDto> Matches { get; set; }
    }
}
