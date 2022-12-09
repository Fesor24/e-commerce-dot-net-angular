using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class GamesWithGenreAndConsoleSpecification: BaseSpecification<Games>
    {
        public GamesWithGenreAndConsoleSpecification(GamesSpecParams specParams): base(
            x =>
            (!specParams.DeviceId.HasValue || x.ConsoleDeviceId == specParams.DeviceId) &&
            (!specParams.GenreId.HasValue || x.GenreId == specParams.GenreId) &&
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search))
            )
        {
            AddIncludes(x => x.Genre);
            AddIncludes(x => x.ConsoleDevice);
            ApplyPaging(specParams.PageSize, (specParams.PageIndex - 1) * specParams.PageSize);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;

                    case "nameDesc":
                        AddOrderByDescending(x => x.Name);
                        break;

                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }
        public GamesWithGenreAndConsoleSpecification(int id): base(x => x.Id == id)
        {
            AddIncludes(x => x.Genre);
            AddIncludes(x => x.ConsoleDevice);
        }
    }
}
