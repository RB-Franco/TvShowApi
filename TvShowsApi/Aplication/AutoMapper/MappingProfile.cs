using AutoMapper;
using Entity.Entity;
using Models.Models;

namespace Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TvShow, TvShowModel>().ReverseMap();
            CreateMap<Episode, EpisodeModel>().ReverseMap();
            CreateMap<Favorite, FavoriteModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<SignUpModel, User>();

        }
    }
}
