using DataAccess.Abstract;
using DataAccess.Concrete.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMovieStarDal : GenericRepository<MovieStar>, IMovieStarDal
    {
       public IQueryable<MovieStar> GetAllWithStars()
    {
        return _context.MovieStars.Include(ms => ms.Star);
    }
    }
}
