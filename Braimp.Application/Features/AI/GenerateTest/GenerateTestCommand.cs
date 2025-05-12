using Braimp.Application.Modules;
using MediatR;

namespace Braimp.Application.Features.AI.GenerateTest;
public record GenerateTestCommand(string Content) : IRequest<AiMessage>;
