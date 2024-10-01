using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using MoviesDatabase.Models;
using MoviesDatabase.Interfaces;
using Microsoft.Identity.Client;

public class ContextDB : DbContext
{
    public DbSet<MovieModel> Movies { get; set; }
    public DbSet<ThemeModel> Themes { get; set; }
    public DbSet<SeatModel> Seats { get; set; }

    public DbSet<CinemaHallModel> CinemaHall { get; set; }

    public DbSet<ActorModel> Actors { get; set; }

    public DbSet<ImageBlobModel> ImageBlobs { get; set; }

    public DbSet<ScheduleModel> Schedules { get; set; }

    public DbSet<DetailsModel> Details { get; set; }

    public DbSet<PersonModel> Persons { get; set; }

    public DbSet<DateModel> Dates { get; set; }


    public DbSet<AdminUserModel> AdminUsers { get; set; }

    public DbSet<MovieThemeModel> MovieThemeConnector { get; set; }


    public ContextDB(DbContextOptions<ContextDB> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}

