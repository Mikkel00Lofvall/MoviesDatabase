using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using MoviesDatabase.Models;
using MoviesDatabase.Interfaces;

public class MovieDBContext : DbContext
{
    public DbSet<MovieModel> Movies { get; set; }

    public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

