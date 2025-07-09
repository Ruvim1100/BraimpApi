using AutoMapper;
using Braimp.Application.Features.Modules.Commands.CreateModule;
using Braimp.Application.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Modules.CreateModule;
public class Request : IMapWith<CreateModuleCommand>
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    public bool IsPublished { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, CreateModuleCommand>();
    }
}
