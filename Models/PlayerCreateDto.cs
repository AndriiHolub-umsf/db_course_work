using Microsoft.AspNetCore.Http;

namespace EsportApp.Models
{
    public class PlayerCreateDto
    {
        public string Name { get; set; }
        public string Dob { get; set; }
        public string Sex { get; set; }
        public IFormFile Photo { get; set; }
    }
}
