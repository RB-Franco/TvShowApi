using Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IApplicationTvShow
    {
        Task<TvShow> SearchById(int id);
        Task<List<TvShow>> SearchAll();
        Task<List<TvShow>> SearchByName(string name);
    }
}
