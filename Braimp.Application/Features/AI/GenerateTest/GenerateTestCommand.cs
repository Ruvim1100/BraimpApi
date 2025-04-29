using Braimp.Application.Common.Dtos;
using MediatR;

namespace Braimp.Application.Features.AI.GenerateTest;
public record GenerateTestCommand(string Content) : IRequest<GenerateTestResponse>;
