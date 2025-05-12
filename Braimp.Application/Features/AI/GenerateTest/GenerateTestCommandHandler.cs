using Braimp.Application.Abstraction;
using MediatR;
using Braimp.Application.Modules;

namespace Braimp.Application.Features.AI.GenerateTest;

public class GenerateTestCommandHandler(IAiService aiService) : IRequestHandler<GenerateTestCommand, AiMessage>
{
    public async Task<AiMessage> Handle(GenerateTestCommand request, CancellationToken cancellationToken)
    {
        var AiMessage = new AiMessage(request.Content);
        return await aiService.GenerateTestAsync(AiMessage, cancellationToken);
    }
}
