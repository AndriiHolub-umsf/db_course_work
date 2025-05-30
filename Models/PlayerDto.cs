namespace EsportApp.Models
{
    public class PlayerDto
    {
        public int Pid { get; set; }
        public string Pname { get; set; }
        public string Dob { get; set; }
        public string Sex { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoBase64 { get; set; }
    }
}
