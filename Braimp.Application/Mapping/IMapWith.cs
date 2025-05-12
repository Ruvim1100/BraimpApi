using AutoMapper;

namespace Braimp.Application.Mapping;
public interface IMapWith<T>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}
