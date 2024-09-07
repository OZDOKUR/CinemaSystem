using Entities.Concrete;

namespace CinemaSystem.Models
{
    public class RemoveMovieStarViewModel
    {
        public Movie Movie { get; set; }
        public List<MovieStar> MovieStars { get; set; }
        public List<Star> Stars { get; set; }
    }
}
