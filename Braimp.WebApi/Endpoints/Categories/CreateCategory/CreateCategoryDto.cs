using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Features.Categories.Commands.CreateCategory;

namespace Braimp.WebApi.Endpoints.Categories.CreateCategory;
public record CreateCategoryDto : IMapWith<CreateCategoryCommand>
{
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCategoryDto, CreateCategoryCommand>();
    }
}