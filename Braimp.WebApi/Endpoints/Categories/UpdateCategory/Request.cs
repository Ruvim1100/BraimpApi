using AutoMapper;
using Braimp.Application.Features.Categories.Commands.UpdateCategory;
using Braimp.Application.Mapping;

namespace Braimp.WebApi.Endpoints.Categories.UpdateCategory;
public record UpdateCategoryDto : IMapWith<UpdateCategoryCommand>
{
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>();
    }
}
