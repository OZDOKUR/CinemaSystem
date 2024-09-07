using DataAccess.Abstract;
using DataAccess.Concrete.Repositories;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : GenericRepository<AppUser>, IUserDal
    {
        public List<AppUser> GetUsersByRoleId(int roleId)
        {
            using (var context = new Context())
            {
                return context.Users
                    .Where(user => context.UserRoles
                        .Any(userRole => userRole.RoleId == roleId && userRole.UserId == user.Id))
                    .ToList();
            }
        }
    }

}
