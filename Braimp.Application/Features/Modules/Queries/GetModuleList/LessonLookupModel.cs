namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class LessonLookupModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public int SortIndex { get; set; }
}
