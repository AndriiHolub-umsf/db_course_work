using Microsoft.AspNetCore.Http;

namespace EsportApp.Models
{
    public class TeamCreateDto
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
    }
}
