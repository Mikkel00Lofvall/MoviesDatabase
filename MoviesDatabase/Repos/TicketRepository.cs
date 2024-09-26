using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class TicketRepository : Repository<TicketModel>
    {
        public TicketRepository(ContextDB context) : base(context) { }

    }
}
