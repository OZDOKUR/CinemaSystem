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
    public class EfSeanceDal : GenericRepository<Seance>, ISeanceDal
    {
        public IQueryable<Seance> GetAllIncluding(Expression<Func<Seance, bool>> filter = null, params string[] includeProperties)
        {
            IQueryable<Seance> query = _context.Set<Seance>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Seance Get(Expression<Func<Seance, bool>> filter, string includeProperties = "")
        {
            IQueryable<Seance> query = _context.Set<Seance>();

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
