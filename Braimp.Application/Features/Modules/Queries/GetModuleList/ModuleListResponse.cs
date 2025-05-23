namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class ModuleListResponse
{
    public IList<ModuleLookupModel> Modules { get; set; } = new List<ModuleLookupModel>();
}
