using Orleans;

namespace Interfaces
{
    public interface IApiList : IGrainWithGuidKey
    {
        Task<List<ApiListModel>> GetApiList(); 
    }
}


public class ApiListModel
{
    public int ApiCode { get; set; }
    public float Version { get; set; }
}
