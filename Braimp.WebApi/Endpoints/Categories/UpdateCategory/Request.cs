using AutoMapper;
using Braimp.Application.Features.Categories.Commands.UpdateCategory;
using Braimp.Application.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Categories.UpdateCategory;
public record UpdateCategoryDto : IMapWith<UpdateCategoryCommand>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCategoryDto, UpdateCategoryCommand>();
    }
}
