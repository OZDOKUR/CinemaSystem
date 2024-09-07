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
    public class MovieStarManager : IMovieStarService
    {
        private readonly IMovieStarDal _movieStarDal;

        public MovieStarManager(IMovieStarDal movieStarDal)
        {
            _movieStarDal = movieStarDal;
        }

        public void Add(MovieStar entity)
        {
            _movieStarDal.Add(entity);
        }

        public void Delete(MovieStar entity)
        {
            _movieStarDal.Delete(entity);
        }

        public List<MovieStar> GetAll()
        {
            return _movieStarDal.GetAllWithStars().ToList();
        }

        public MovieStar GetById(int id)
        {
            return _movieStarDal.GetById(id);
        }

        public void Update(MovieStar entity)
        {
            var existingMovieStar = _movieStarDal.GetById(entity.MovieId);
            if (existingMovieStar != null)
            {
                existingMovieStar.StarId = entity.StarId;

                _movieStarDal.Update(existingMovieStar);
            }
        }
    }

}
