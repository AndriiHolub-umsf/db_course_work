namespace EsportApp.Models
{
    public class CreateMatchDto
    {
        public int TeamAId { get; set; }
        public int TeamBId { get; set; }
        public int Round { get; set; }
    }
}
