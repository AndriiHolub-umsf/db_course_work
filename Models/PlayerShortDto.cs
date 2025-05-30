namespace EsportApp.Models
{
    public class PlayerShortDto
    {
        public int Pid { get; set; }        
        public string Pname { get; set; }    
        public byte[] Photo { get; set; } 
        public string PhotoBase64 { get; set; } 
    }
}
