using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public int SeanceId { get; set; }
        public decimal TicketPrice { get; set; }
        public int UserId { get; set; }
        public int WorkerId { get; set; }
        public DateTime SellTime { get; set; }

        
        public Seance Seance { get; set; }

     
        public AppUser User { get; set; }

       
        public AppUser Worker { get; set; }

     
 
    }
}
