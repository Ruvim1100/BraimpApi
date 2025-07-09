using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionList;
public class GetSubmissionListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetSubmissionListQuery, SubmissionListResponse>
{
    public async Task<SubmissionListResponse> Handle(GetSubmissionListQuery request, CancellationToken cancellationToken)
    {
        var submissions = await dbContext.Submissions
                    .Where(s => s.AssignmentId == request.AssignmentId)
                    .ProjectTo<SubmissionLookupModel>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

        var studentIds = submissions
                    .Select(student => student.StudentId)
                    .Distinct()
                    .ToList();

        var students = await dbContext.Users
            .Where(user => studentIds.Contains(user.Id))
            .ToDictionaryAsync(user => user.Id, cancellationToken);

        foreach (var student in submissions)
        {
            if (students.TryGetValue(student.StudentId, out var user))
            {
                student.Student = mapper.Map<StudentModel>(user);
            }
        }

        return new SubmissionListResponse
        {
            Submissions = submissions
        };
    }
}
