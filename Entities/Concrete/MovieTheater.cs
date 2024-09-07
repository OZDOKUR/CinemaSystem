using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MovieTheater
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int TheatherNumber { get; set; }
        public int Capacity { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Seance> Seances { get; set; }
    }
}
