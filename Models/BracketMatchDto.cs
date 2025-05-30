namespace EsportApp.Models
{
    public class BracketMatchDto
    {
        public int Id { get; set; }
        public int? TeamAId { get; set; }
        public string TeamAName { get; set; }
        public string TeamALogoBase64 { get; set; }
        public int? TeamAScore { get; set; }
        public int? TeamBId { get; set; }
        public string TeamBName { get; set; }
        public string TeamBLogoBase64 { get; set; }
        public int? TeamBScore { get; set; }
        public int? WinnerId { get; set; }
        public bool Finished { get; set; }
    }
}
