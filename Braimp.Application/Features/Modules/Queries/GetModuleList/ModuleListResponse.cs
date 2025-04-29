namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class ModuleListResponse
{
    public IList<ModuleLookupDto> Modules { get; set; } = new List<ModuleLookupDto>();
}
