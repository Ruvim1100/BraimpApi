using AutoMapper;
using Braimp.Application.Features.Categories.Commands.CreateCategory;
using Braimp.Application.Mapping;

namespace Braimp.WebApi.Endpoints.Categories.CreateCategory;
public record CreateCategoryDto : IMapWith<CreateCategoryCommand>
{
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>();
    }
}