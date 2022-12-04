using Core.Entities;

namespace API.DTOs
{
    public class GamesToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Decimal Price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string ConsoleDevice { get; set; } = string.Empty;
    }
}
