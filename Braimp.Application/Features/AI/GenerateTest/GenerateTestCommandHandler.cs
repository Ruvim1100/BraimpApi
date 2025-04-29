using Braimp.Application.Common.Dtos;
using Braimp.Application.Abstraction;
using MediatR;

namespace Braimp.Application.Features.AI.GenerateTest;

public class GenerateTestCommandHandler(IAiService aiService) : IRequestHandler<GenerateTestCommand, GenerateTestResponse>
{
    public async Task<GenerateTestResponse> Handle(GenerateTestCommand request, CancellationToken cancellationToken)
    {
        var generateTestRequest = new GenerateTestRequest(request.Content);
        return await aiService.GenerateTestAsync(generateTestRequest, cancellationToken);
    }
}
