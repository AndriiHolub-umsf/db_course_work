using System;

namespace EsportApp.Models
{
    public class TournamentDetailsDto
    {
        public int Id { get; set; }             
        public string Gname { get; set; }       
        public string Name { get; set; }         
        public DateTime Date { get; set; }      
        public int Max_teams { get; set; }        
        public int Points_to_win { get; set; }    
        public int Players_in_team { get; set; }
        public byte[] Game_photo { get; set; }
        public string GamePhotoBase64 { get; set; }
    }
}
