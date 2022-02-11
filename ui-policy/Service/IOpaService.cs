using System.Threading.Tasks;
using ui_policy.Dto;

namespace ui_policy.Service
{
    public interface IOpaService
    {
        Task<OpaQueryResponse> QueryOpaAsync(OpaQueryRequest queryRequest);
    }
}