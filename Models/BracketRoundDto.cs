using System.Collections.Generic;

namespace EsportApp.Models
{
    public class BracketRoundDto
    {
        public int RoundNumber { get; set; }
        public List<BracketMatchDto> Matches { get; set; }
    }
}
