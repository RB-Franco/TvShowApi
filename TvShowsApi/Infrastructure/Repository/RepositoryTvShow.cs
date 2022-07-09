using Domain.Interfaces;
using Entity.Entity;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryTvShow : ITvShow
    {
        private DbContextOptions<Context> _OptionsBuilder;

        public RepositoryTvShow()
        {
            _OptionsBuilder = new DbContextOptions<Context>();
        }

        public async Task<IEnumerable<TvShow>> SearchAll()
        {
            using (var context = new Context(_OptionsBuilder))
            {
                var result = await context.TvShow
                                        .AsNoTracking()
                                        .ToListAsync();
                return result;              
            }
        }

        public async Task<TvShow> SearchById(int Id)
        {
            using (var context = new Context(_OptionsBuilder))
            {
                var result = await context.TvShow.FindAsync(Id);
                return result;
            }
        }

        public async Task<IEnumerable<TvShow>> SearchByName(string name)
        {
            using (var context = new Context(_OptionsBuilder))
            {
                var result = await context.TvShow
                                        .Where(x => x.Name.Equals(name))
                                        .AsNoTracking()
                                        .ToListAsync();
                return result;
            }
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesByUserId(string userId)
        {
            using (var context = new Context(_OptionsBuilder))
            {
                var result = await context.Favorites
                                        .Where(x=> x.UserId.Equals(userId))
                                        .AsNoTracking()
                                        .ToListAsync();
                return result;                
            }
        }

        public async Task<TvShow> AddTvShowToFavorites(TvShow tvShow, string userId)
        {

            using (var context = new Context(_OptionsBuilder))
            {
                await context.Favorites.AddAsync(new Favorite
                {
                    ShowId = tvShow.Id,
                    UserId = userId

                });
                await context.SaveChangesAsync();

            }
            return tvShow;
        }

        public async Task<bool> RemoveTvShowToFavorites(Favorite favorite)
        {
            using (var context = new Context(_OptionsBuilder))
            {
                context.Favorites.Remove(favorite);
                await context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<IEnumerable<Episode>> GetEpisodesByTvShowId(int tvShowId)
        {
            using (var context = new Context(_OptionsBuilder))
            {
                var result = await context.Episode
                         .Where(x => x.ShowId.Equals(tvShowId))
                         .AsNoTracking()
                         .ToListAsync();

                return result;
            }
        }
    }
}
