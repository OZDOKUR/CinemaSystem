using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSystem.Controllers.Admin.Controllers
{

    public class AdminDashboardController : Controller
    {
        private TicketManager _ticketManager = new TicketManager(new EfTicketDal());

        [Authorize(Policy = "AdminRole")]
        public IActionResult Index()
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);

            ViewBag.DailyTotal = _ticketManager.GetTotalTicketPriceByDate(today);
            ViewBag.MonthlyTotal = _ticketManager.GetTotalTicketPriceByMonth(today);

            var tickets = _ticketManager.GetAll();
            return View(tickets);
        }
    }
}
