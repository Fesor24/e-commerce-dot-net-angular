using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class GamesWithFiltersCountSpecification : BaseSpecification<Games>
    {
        public GamesWithFiltersCountSpecification(GamesSpecParams specParams)
            :base(x => 
            (!specParams.GenreId.HasValue || x.GenreId == specParams.GenreId) &&
            (!specParams.DeviceId.HasValue || x.ConsoleDeviceId == specParams.DeviceId) &&
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search))
            )
        {

        }
    }
}
