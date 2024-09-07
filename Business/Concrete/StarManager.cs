using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StarManager : IStarService
    {
        private IStarDal _starDal;

       

        public StarManager(IStarDal starDal)
        {
            _starDal = starDal;
        }

        public void Add(Star entity)
        {
            _starDal.Add(entity);
        }

        public void Delete(Star entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var starId = _starDal.GetById(id);
            _starDal.Delete(starId);
        }

        public List<Star> GetAll()
        {
            return _starDal.List();
        }

        public Star GetById(int id)
        {
            return _starDal.Get(x => x.Id == id);
        }

        public void Update(Star entity)
        {
            Star resultStar = _starDal.GetById(x => x.Id == entity.Id);
            resultStar.Name = entity.Name;
            resultStar.Picture = entity.Picture;
            _starDal.Update(resultStar);
        }
    }
}
