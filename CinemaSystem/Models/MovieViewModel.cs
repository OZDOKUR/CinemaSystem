using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaSystem.Models
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Movie> MovieList { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Stars { get; set; }
    }
}
