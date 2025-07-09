using AutoMapper;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Queries.GetQuizDetails;
public class GetQuizDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper) : IRequestHandler<GetQuizDetailsQuery, QuizDetailsResponse>
{
    public async Task<QuizDetailsResponse> Handle(GetQuizDetailsQuery request, CancellationToken cancellationToken)
    {
        var quiz = await dbContext.Quizzes
            .Include(quiz => quiz.Questions)
            .FirstAsync(quiz => quiz.Id == request.Id, cancellationToken);

        return mapper.Map<QuizDetailsResponse>(quiz);
    }
}
