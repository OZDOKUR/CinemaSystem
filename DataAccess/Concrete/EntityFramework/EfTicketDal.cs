using DataAccess.Abstract;
using DataAccess.Concrete.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTicketDal : GenericRepository<Ticket>, ITicketDal
    {
      
        public EfTicketDal() : base()
        {
        }

        public decimal GetTotalTicketPriceByDate(DateTime date)
        {
            return _context.Set<Ticket>()
                .Where(t => t.SellTime.Date == date.Date)
                .Sum(t => t.TicketPrice);
        }

        public decimal GetTotalTicketPriceByMonth(DateTime date)
        {
            return _context.Set<Ticket>()
                .Where(t => t.SellTime.Year == date.Year && t.SellTime.Month == date.Month)
                .Sum(t => t.TicketPrice);
        }
    }
}
