using Microsoft.AspNetCore.Http;

namespace EsportApp.Models
{
    public class GameEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
