using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private IRoleDal _roleDal;

        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        public void Add(AppRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(AppRole entity)
        {
            throw new NotImplementedException();
        }

        public List<AppRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public AppRole GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AppRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
