using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class TicketRepository : Repository<TicketModel>
    {
        public TicketRepository(ContextDB context) : base(context) { }

        public virtual async Task<(bool, string)> Create(TicketModel ticket)
        {
            var result = await GetAll();
            return (true, "");
        }


        public override async Task<(bool, string)> Delete(TicketModel ticket)
        {
            string lol = await GetFun();
            return (true, lol);
        }

        private async Task<string> GetFun()
        {
            return "lol";
        }

    }
}
