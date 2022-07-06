using Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces

{
    public interface ITvShow
    {
        Task<TvShow> SearchById(int Id);

        Task<List<TvShow>> SearchAll();

        Task<List<TvShow>> SearchByName(string name);
    }
}
