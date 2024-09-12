using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ContextDBFactory : IDesignTimeDbContextFactory<ContextDB>
{
    public ContextDB CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContextDB>();

        // Replace with your actual connection string
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MovieProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        return new ContextDB(optionsBuilder.Options);
    }
}
