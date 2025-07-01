using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Queries.GetPublishedQuizzes;
public class GetPublishedQuizListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) : IRequestHandler<GetPublishedQuizListQuery, PublishedQuizListResponse>
{
    public async Task<PublishedQuizListResponse> Handle(GetPublishedQuizListQuery request, CancellationToken cancellationToken)
    {
        var quizzes = await dbContext.Quizzes
            .Where(quiz => quiz.CourseId == request.CourseId && quiz.IsPublished)
            .OrderBy(quiz => quiz.CreatedAt)
            .ProjectTo<PublishedQuizLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PublishedQuizListResponse { Quizzes = quizzes};
    }
}
