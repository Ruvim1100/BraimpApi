using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Queries.GetQuizzes;
public class GetQuizListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetQuizListQuery, QuizListResponse>
{
    public async Task<QuizListResponse> Handle(GetQuizListQuery request, CancellationToken cancellationToken)
    {
        var quizzes = await dbContext.Quizzes
            .Where(quiz => quiz.CourseId == request.CourseId)
            .OrderBy(quiz => quiz.SortIndex)
            .ProjectTo<QuizLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new QuizListResponse { Quizzes = quizzes };
    }
}
