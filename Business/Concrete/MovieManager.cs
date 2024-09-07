using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private IMovieDal _movieDal;
        private ISeanceDal _seanceDal;

        public MovieManager(IMovieDal movieDal)
        {
            _movieDal = movieDal;
        }

        public void Add(Movie movie)
        {
            _movieDal.Add(movie);
        }

        public void Delete(int id)
        {
            var movie = _movieDal.GetById(x => x.Id == id);
            if (movie != null)
            {
                _movieDal.Delete(movie);
            }
        }
        
        public void Delete(Movie entity)
        {
            _movieDal.Delete(entity);
        }

        public List<Movie> GetAll()
        {
            return _movieDal.List();
        }

        public Movie GetById(int id)
        {
            
            return _movieDal.Get(m => m.Id == id, includeProperties: "Category,MovieStars.Star");
        }
        public Movie GetByName(string name)
        {

            return _movieDal.Get(x => x.Name == name, includeProperties: "Category,MovieStars.Star");
        }

        public int GetIdByName(string name)
        {
            var movie = GetByName(name);
            return movie.Id;
        }


        public void Update(Movie movie)
        {
            Movie resultMovie = _movieDal.Get(x => x.Id == movie.Id, includeProperties: "MovieStars");
            if (resultMovie != null)
            {
                resultMovie.Name = movie.Name;
                resultMovie.CategoryId = movie.CategoryId;
                resultMovie.ScreenTime = movie.ScreenTime;
                resultMovie.Picture = movie.Picture;
                resultMovie.Director = movie.Director;
                resultMovie.DirectorPicture = movie.DirectorPicture;
                resultMovie.Score = movie.Score;
                resultMovie.Description = movie.Description;
                resultMovie.Is3D = movie.Is3D;
                resultMovie.IsDubbed = movie.IsDubbed;
                resultMovie.IsLocal = movie.IsLocal;
                resultMovie.Trailer = movie.Trailer;
                resultMovie.MovieStars = movie.MovieStars;
                resultMovie.Background = movie.Background;

                _movieDal.Update(resultMovie);
            }
        }
    }
}
