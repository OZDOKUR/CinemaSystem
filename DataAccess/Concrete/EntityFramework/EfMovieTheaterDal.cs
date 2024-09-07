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
    public class EfMovieTheaterDal : GenericRepository<MovieTheater>, IMovieTheaterDal
    {
        public MovieTheater Get(Expression<Func<MovieTheater, bool>> filter, string includeProperties = "")
        {
            IQueryable<MovieTheater> query = _context.Set<MovieTheater>();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query.SingleOrDefault(filter);
        }

        public List<MovieTheater> List(string includeProperties = "")
        {
            IQueryable<MovieTheater> query = _context.Set<MovieTheater>();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query.ToList();
        }
    }
    
}
