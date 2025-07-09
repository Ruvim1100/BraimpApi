namespace Braimp.Application.Features.Tags.Queries.GetTagList;
public class TagListResponse
{
    public List<TagLookupModel> Tags { get; set; } = new List<TagLookupModel>();
}
