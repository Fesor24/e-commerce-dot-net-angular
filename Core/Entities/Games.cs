using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Games: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Decimal Price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public ConsoleDevice ConsoleDevice { get; set; }
        public int ConsoleDeviceId { get; set; }
    }
}
