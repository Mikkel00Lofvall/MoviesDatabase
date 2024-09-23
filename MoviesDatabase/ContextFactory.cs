using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ContextDBFactory : IDesignTimeDbContextFactory<ContextDB>
{
    private readonly string _connectionString;

    public ContextDBFactory(string ConnectionString) 
    {
        this._connectionString = ConnectionString;
    }

    public ContextDB CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContextDB>();
        optionsBuilder.UseSqlServer(_connectionString);

        return new ContextDB(optionsBuilder.Options);
    }
}
