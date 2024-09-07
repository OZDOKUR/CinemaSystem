using Entities.Concrete;

namespace CinemaSystem.Models
{

    public class WorkerViewModel
    {
        public List<AppUser> Users { get; set; }
        public RegisterViewModel NewWorker { get; set; }

    }
}
