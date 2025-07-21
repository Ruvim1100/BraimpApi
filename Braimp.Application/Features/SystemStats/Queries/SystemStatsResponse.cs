namespace Braimp.Application.Features.SystemStats.Queries;
public class SystemStatsResponse
{
    public int TotalCourses { get; set; }
    public int PublishedCourses { get; set; }
    public int TotalUsers { get; set; }
    public int NewUsersLast7Days { get; set; }
}
