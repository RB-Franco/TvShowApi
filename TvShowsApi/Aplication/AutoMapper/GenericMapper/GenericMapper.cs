using AutoMapper;

namespace Application.AutoMapper.GenericMapper
{
    public class GenericMapper<TModel, TEntidade>
    {
        protected virtual TEntidade Converta(TModel model) => Mapper.Map<TModel, TEntidade>(model);
        protected virtual TModel Converta(TEntidade model) => Mapper.Map<TEntidade, TModel>(model);
    }
}
