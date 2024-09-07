using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Background { get; set; }
        public string ScreenTime { get; set; }
        public string Director { get; set; }
        public string DirectorPicture { get; set; }
        public double Score { get; set; }
        public string Description { get; set; }
      
        public bool Is3D { get; set; }
        public bool IsDubbed { get; set; }
        public bool IsLocal { get; set; }

        public string Trailer { get; set; }

        // Foreign Key
        public int? CategoryId { get; set; }
        // Navigation Property
        public Category Category { get; set; }


        public ICollection<MovieStar> MovieStars { get; set; }

        public ICollection<Seance> Seances { get; set; }

    }
   
}
