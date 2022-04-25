using Orleans;

namespace Interfaces
{
    public interface IProfile : IGrainWithGuidKey
    {
        Task<ProfileModel> GetProfile();
    }
}


public class ProfileModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
