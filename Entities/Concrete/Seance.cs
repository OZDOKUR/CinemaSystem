using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Seance
    {
        [Key]
        public int Id { get; set; }
        public TimeSpan Hours { get; set; }

      
        public int MovieId { get; set; }
       
        public Movie Movie { get; set; }

     
        public int MovieTheaterId { get; set; }
      
        public MovieTheater MovieTheater { get; set; }
        public DateTime Date { get; set; }
        public string SeatsOccupied { get; set; }

        public bool Status { get; set; }


        public ICollection<Ticket> Tickets { get; set; }
    }

}
