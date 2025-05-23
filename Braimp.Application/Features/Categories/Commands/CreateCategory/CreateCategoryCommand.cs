using MediatR;
namespace Braimp.Application.Features.Categories.Commands.CreateCategory;
public record CreateCategoryCommand(string Name) : IRequest<Guid>;