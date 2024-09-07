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
    public class MovieTheaterManager : IMovieTheaterService
    {
        IMovieTheaterDal _movieTheaterDal;

        public MovieTheaterManager(IMovieTheaterDal movieTheaterDal)
        {
            _movieTheaterDal = movieTheaterDal;
        }

        public void Add(MovieTheater entity)
        {
            _movieTheaterDal.Add(entity);
        }

        public void Delete(MovieTheater entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            var theaterId = _movieTheaterDal.GetById(id);
            _movieTheaterDal.Delete(theaterId);
        }

        public List<MovieTheater> GetAll()
        {
            return _movieTheaterDal.List();
        }

        public MovieTheater GetById(int id)
        {
            return _movieTheaterDal.Get(m => m.Id == id);
        }

        public void Update(MovieTheater theater)
        {
            MovieTheater resultTheater = _movieTheaterDal.GetById(x =>x.Id == theater.Id);
            resultTheater.Name = theater.Name;
            resultTheater.Country = theater.Country;
            resultTheater.TheatherNumber = theater.TheatherNumber;
            resultTheater.Capacity = theater.Capacity;

           
            _movieTheaterDal.Update(resultTheater);
        }
    }
}
