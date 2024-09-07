using Core.DataAccess;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ITicketDal : IEntityRepository<Ticket>
    {
        decimal GetTotalTicketPriceByDate(DateTime date);
        decimal GetTotalTicketPriceByMonth(DateTime date);

    }
}
