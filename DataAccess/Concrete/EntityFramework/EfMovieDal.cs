using DataAccess.Abstract;
using DataAccess.Concrete.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMovieDal : GenericRepository<Movie>, IMovieDal
    {

        public Movie Get(Expression<Func<Movie, bool>> filter, string includeProperties = "")
        {
            IQueryable<Movie> query = _context.Set<Movie>();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query.SingleOrDefault(filter);
        }
    }
}
