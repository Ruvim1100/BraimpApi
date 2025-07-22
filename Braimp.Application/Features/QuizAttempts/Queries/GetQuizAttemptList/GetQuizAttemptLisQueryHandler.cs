using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizAttempts.Queries.GetQuizAttemptList;
public class GetQuizAttemptLisQueryHandler(IBraimpDbContext dbContext, IMapper mapper,
    ICurrentUserService currentUserService) : IRequestHandler<GetQuizAttemptListQuery, QuizAttemptListResponse>
{
    public async Task<QuizAttemptListResponse> Handle(GetQuizAttemptListQuery request, CancellationToken cancellationToken)
    {
        var quizAttempts = await dbContext.QuizAttempts
            .Where(quizAttempt => quizAttempt.StudentId == currentUserService.UserId &&
            quizAttempt.QuizId == request.QuizId)
            .ProjectTo<QuizAttemptLookupModel>(mapper.ConfigurationProvider)
            .OrderBy(quizAttempt => quizAttempt.AttemptNumber)
            .ToListAsync(cancellationToken);

         return new QuizAttemptListResponse { QuizAttempts = quizAttempts };
    }
}
