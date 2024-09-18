using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using MoviesDatabase.Models;
using MoviesDatabase.Interfaces;

public class ContextDB : DbContext
{
    public DbSet<MovieModel> Movies { get; set; }
    public DbSet<ThemeModel> Themes { get; set; }
    public DbSet<Seat> Seats { get; set; }

    public DbSet<CinemaHallModel> CinemaHall { get; set; }

    public DbSet<ActorModel> Actors { get; set; }

    public DbSet<ImageBlobModel> ImageBlobs { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public ContextDB(DbContextOptions<ContextDB> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}

