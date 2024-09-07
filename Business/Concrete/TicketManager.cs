using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class TicketManager : ITicketService
    {
        private readonly ITicketDal _ticketDal;

        public TicketManager(ITicketDal ticketDal)
        {
            _ticketDal = ticketDal;
        }

        
        public void Add(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            _ticketDal.Add(ticket);
        }

       
        public void Delete(Ticket entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _ticketDal.Delete(entity);
        }

      
        public List<Ticket> GetAll()
        {
            return _ticketDal.List();
        }
        


        public Ticket GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }

            var ticket = _ticketDal.GetById(x => x.Id == id);
            return ticket;
        }

        public decimal GetTotalTicketPriceByDate(DateTime date)
        {
            return _ticketDal.GetTotalTicketPriceByDate(date);
        }

        public decimal GetTotalTicketPriceByMonth(DateTime date)
        {
            return _ticketDal.GetTotalTicketPriceByMonth(date);
        }
        public void Update(Ticket entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _ticketDal.Update(entity);
        }
    }
}
