using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MovieStar
    {
        [Key]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int StarId { get; set; }
        public Star Star { get; set; }
    }
}
