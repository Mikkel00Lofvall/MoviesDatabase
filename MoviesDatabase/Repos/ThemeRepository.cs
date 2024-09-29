using Microsoft.EntityFrameworkCore;
using MoviesDatabase.DTO;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Repos
{
    public class ThemeRepository : Repository<ThemeModel>
    {
        public ThemeRepository(ContextDB context) : base(context) { }

        public async Task<(bool, string)> Delete(int ThemeID)
        {
            try
            {
                var theme = await _context.Themes.FindAsync(ThemeID);

                if (theme == null)
                {
                    return (false, "Theme not found.");
                }

                var movieThemeEntries = await _context.MovieThemeConnector
                    .Where(mt => mt.ThemeID == ThemeID)
                    .ToListAsync();

                if (movieThemeEntries.Any())
                {
                    _context.MovieThemeConnector.RemoveRange(movieThemeEntries);
                    await _context.SaveChangesAsync();
                }

                _context.Themes.Remove(theme);
                await _context.SaveChangesAsync();

                return (true, "Theme and related associations deleted successfully.");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }

        public async Task<(bool, string)> UpdateMovieWithThemes(int MovieID, UpdateThemeDTO ThemeDTO)
        {
            try
            {
                if (ThemeDTO == null)
                {
                    return (false, "No ThemeDTO");
                }

                if (MovieID == null || MovieID < 0) 
                {
                    return (false, "MovieID was either null or less than 0");    
                }

                var movie = await _context.Movies
                    .Include(m => m.MovieThemeConnector)
                    .FirstOrDefaultAsync(m => m.id == MovieID);

                if (movie == null)
                {
                    return (false, "No Movie with that id in database");
                }

                var currentThemeIds = movie.MovieThemeConnector.Select(mt => mt.ThemeID).ToList();

                var themesToAdd = ThemeDTO.ThemeIds.Except(currentThemeIds).ToList();
                var themesToRemove = currentThemeIds.Except(ThemeDTO.ThemeIds).ToList();

                foreach (var themeID in themesToRemove) 
                {
                    var movieThemeToRemove = movie.MovieThemeConnector.FirstOrDefault(mt => mt.ThemeID == themeID);
                    if (movieThemeToRemove != null) 
                    { 
                        _context.MovieThemeConnector.Remove(movieThemeToRemove);
                    }
                }

                foreach (var themedID  in themesToAdd)
                {
                    var ThemeExist = await _context.Themes.AnyAsync(t =>  t.id == themedID);
                    if (ThemeExist) 
                    {
                        if (!movie.MovieThemeConnector.Any(mt => mt.ThemeID == themedID))
                        {
                            movie.MovieThemeConnector.Add(new MovieThemeModel { MovieID = movie.id, ThemeID = themedID });
                        }
                    }

                    else
                    {
                        return (false, "No Theme With that id");
                    }
                }

                await _context.SaveChangesAsync();

                return (true, "");

            }
            catch (Exception ex) { return (false, ex.Message); }
        }


        public async Task<(bool, string, ICollection<ThemeModel>)> GetThemesWithMovieID(int MovieID)
        {
            try
            {
                var themeIds = await _context.Set<MovieThemeModel>()
                    .Where(mt => mt.MovieID == MovieID)
                    .Select(mt => mt.ThemeID)
                    .ToListAsync();

                var themes = await _context.Set<ThemeModel>()
                    .Where(t => themeIds.Contains(t.id))
                    .ToListAsync();

                return (true, "", themes);

            }
            catch (Exception ex) { return (false, ex.Message, null); }
        }
    }
}
