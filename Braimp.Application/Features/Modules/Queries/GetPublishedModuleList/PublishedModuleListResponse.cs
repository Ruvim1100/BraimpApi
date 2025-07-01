namespace Braimp.Application.Features.Modules.Queries.GetPublishedModuleList;
public class PublishedModuleListResponse
{
    public IList<PublishedModuleLookupModule> Modules { get; set; } 
        = new List<PublishedModuleLookupModule>();
}