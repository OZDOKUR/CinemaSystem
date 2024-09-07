using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaSystem.Models
{
    public class MovieTheaterViewModel
    {
        public MovieTheater MovieTheater { get; set; }
        public List<MovieTheater> MovieTheaterList { get; set; }
        public SelectList Cities { get; set; }
    }
}
