using Application.Entiries;

namespace Application.Repositories
{
    public interface IProfileRepository
    {
        Tuple<List<Profile>, int> GetAllProfiles(int page, string search);

        Task<List<string>> ExtensionsList();
    }
}
