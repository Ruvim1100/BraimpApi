namespace Braimp.Application.Features.Courses.Queries.GetStudentList;
public class StudentListResponse
{
    public List<StudentLookupModel> Students { get; set; } = 
        new List<StudentLookupModel>();
}
