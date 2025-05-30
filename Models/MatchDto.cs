namespace EsportApp.Models
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int Round { get; set; }
        public int Team_a_id { get; set; }
        public string TeamA { get; set; }
        public int Team_b_id { get; set; }
        public string TeamB { get; set; }
        public int? ScoreA { get; set; }
        public int? ScoreB { get; set; }
        public int? WinnerId { get; set; }
    }
}
