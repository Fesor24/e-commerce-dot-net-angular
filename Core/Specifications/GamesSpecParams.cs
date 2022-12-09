using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class GamesSpecParams
    {
        private const int MaxPageSize = 50;

        //By default, we will return the first page
        public int PageIndex { get; set; } = 1;

        //By default we are setting the page size to 6
        private int _pageSize = 6;

        public int PageSize {
            get => _pageSize;

            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; 
        }

        public int? GenreId { get; set; }

        public int? DeviceId { get; set; }

        public string? Sort { get; set; }

        private string? _search;
        public string? Search {
            get => _search;

            set => _search = value.ToLower();
        }
    }
}
