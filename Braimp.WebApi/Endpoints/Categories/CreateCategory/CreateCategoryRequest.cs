using AutoMapper;
using Braimp.Application.Features.Categories.Commands.CreateCategory;
using Braimp.Application.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Categories.CreateCategory;
public record Request : IMapWith<CreateCategoryCommand>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, CreateCategoryCommand>();
    }
}