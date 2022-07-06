using Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.Interface
{
    public interface IAplicationTvShow
    {
        Task<TvShow> SearchById(int id);
        Task<List<TvShow>> SearchAll();
        Task<List<TvShow>> SearchByName(string name);
    }
}
