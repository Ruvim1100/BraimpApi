using MediatR;

namespace Braimp.Application.Features.News.Queries.GetCourseNewsList;
public class GetCourseNewsListQuery : IRequest<CourseNewsListResponse>
{
    public Guid CourseId { get; set; }
}
