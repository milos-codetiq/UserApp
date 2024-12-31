using AutoMapper;

namespace Api.Infrastructure.Automapper;

public interface IMapFrom<T>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}
