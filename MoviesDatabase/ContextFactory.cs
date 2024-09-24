using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ContextDBFactory : IDesignTimeDbContextFactory<ContextDB>
{
    private string _connectionString;

    public ContextDBFactory() { }
    public ContextDBFactory(string ConnectionString) 
    {
        this._connectionString = ConnectionString;
    }

    public ContextDB CreateDbContext(string[] args)
    {
        if (_connectionString == null)
        {
            this._connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MovieProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }
        var optionsBuilder = new DbContextOptionsBuilder<ContextDB>();
        optionsBuilder.UseSqlServer(_connectionString);

        return new ContextDB(optionsBuilder.Options);
    }
}
