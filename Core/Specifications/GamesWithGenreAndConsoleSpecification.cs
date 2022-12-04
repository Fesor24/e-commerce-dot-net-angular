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
        public GamesWithGenreAndConsoleSpecification()
        {
            AddIncludes(x => x.Genre);
            AddIncludes(x => x.ConsoleDevice);
        }
        public GamesWithGenreAndConsoleSpecification(int id): base(x => x.Id == id)
        {
            AddIncludes(x => x.Genre);
            AddIncludes(x => x.ConsoleDevice);
        }
    }
}
