using Microsoft.AspNetCore.Http;

namespace EsportApp.Models
{
    public class GameCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
