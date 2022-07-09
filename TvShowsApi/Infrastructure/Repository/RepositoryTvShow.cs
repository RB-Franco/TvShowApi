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
            using (var date = new Context(_OptionsBuilder))
            {
                return await date.Set<TvShow>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<TvShow> SearchById(int Id)
        {
            using (var date = new Context(_OptionsBuilder))
            {
                return await date.Set<TvShow>().FindAsync(Id);
            }
        }

        public async Task<IEnumerable<TvShow>> SearchByName(string name)
        {
            using (var date = new Context(_OptionsBuilder))
            {
                var result = await date.Set<TvShow>()
                                        .Where(x=> x.Name.Equals(name))
                                        .AsNoTracking()
                                        .ToListAsync();
                return result;
            }
        }

        public async Task<TvShow> AddTvShowToFavorites(string name)
        {
            return null;
        }

        public async Task<TvShow> RemoveTvShowToFavorites(string name)
        {
            return null;

        }

        public async Task<IEnumerable<Episode>> GetEpisodesByTvShow(string name)
        {
            return null;

        }

    }
}
