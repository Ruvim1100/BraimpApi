using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetStudentList;
public class GetStudentListQueryHandler(IBraimpDbContext braimpDbContext, IMapper mapper)
    : IRequestHandler<GetStudentListQuery, StudentListResponse>
{
    public async Task<StudentListResponse> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        var students = await braimpDbContext.Users.Where(user =>
        user.Courses.Any(courseParticipant => 
        courseParticipant.CourseId == request.CourseId &&
        courseParticipant.Role == CourseRole.Student))
            .ProjectTo<StudentLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new StudentListResponse { Students = students };

    }
}
