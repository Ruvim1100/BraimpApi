using MediatR;

namespace Braimp.Application.Features.Categories.Commands.UpdateCategory;
public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Unit>;