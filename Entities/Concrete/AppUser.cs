using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Entities.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Ticket> UserTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> WorkerTickets { get; set; } = new List<Ticket>();
    }
}
