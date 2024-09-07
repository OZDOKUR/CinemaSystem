using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISeanceDal : IEntityRepository<Seance>
    {
        IQueryable<Seance> GetAllIncluding(Expression<Func<Seance, bool>> filter = null, params string[] includeProperties);
        Seance Get(Expression<Func<Seance, bool>> filter, string includeProperties = "");
    }

}
