using MoviesDatabase.Interfaces;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoviesDatabase.Repos
{
    public class MovieRepo : IMovieRepo
    {
        private readonly MovieDBContext _context;

        public MovieRepo(MovieDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieModel>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }
        public async Task<MovieModel> GetMovieById(int id)
        {
            return await _context.Movies.FindAsync(id);
        }
        public async Task<bool> CreateMovie(MovieModel movie)
        {
            try
            {
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine("Failure: ", ex);
                return false; 
            }
        }
        public async Task<bool> DeleteMovie(int íd)
        {
            var foundMovie = await _context.Movies.FindAsync(íd);
            if (foundMovie != null) 
            {
                _context.Movies.Remove(foundMovie);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
