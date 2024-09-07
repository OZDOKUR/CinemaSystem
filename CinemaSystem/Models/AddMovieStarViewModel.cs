using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models
{
    public class AddMovieStarViewModel
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int StarId { get; set; }

        public List<SelectListItem> Movies { get; set; }
        public List<SelectListItem> Stars { get; set; }
        public List<MovieStar> ExistingMovieStars { get; set; }
    }
}
