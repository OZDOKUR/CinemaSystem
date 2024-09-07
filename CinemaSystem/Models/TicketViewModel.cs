namespace CinemaSystem.Models
{
    public class TicketViewModel
    {
        public int SeanceId { get; set; }
        public decimal TicketPrice { get; set; }
        public string SelectedSeats { get; set; }
    }
}
