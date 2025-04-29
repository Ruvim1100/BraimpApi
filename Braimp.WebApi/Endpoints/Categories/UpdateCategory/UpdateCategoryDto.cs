using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Features.Categories.Commands.UpdateCategory;

namespace Braimp.WebApi.Endpoints.Categories.UpdateCategory;
public record UpdateCategoryDto : IMapWith<UpdateCategoryCommand>
{
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>();
    }
}
