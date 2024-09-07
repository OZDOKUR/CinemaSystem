using Entities.Concrete;

namespace CinemaSystem.Models
{
    public class SeatPlanViewModel
    {
        public int SeanceId { get; set; }
        public string SelectedSeats { get; set; } 
        public string OccupiedSeats { get; set; } 
        public decimal TotalPrice { get; set; } 
    }
}
