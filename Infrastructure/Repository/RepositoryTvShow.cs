using Domain.Interfaces;
using Entity.Entity;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<List<TvShow>> SearchAll()
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

        public async Task<List<TvShow>> SearchByName(string name)
        {
            using (var date = new Context(_OptionsBuilder))
            {
                var result = await date.Set<TvShow>().AsNoTracking().ToListAsync();
                return result.FindAll(x => x.Name == name);
            }
        }

    }
}
