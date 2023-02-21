using Application.Entiries;
using Application.Entiries.DataContext;
using System.Xml.Linq;

namespace Application.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AnalysisDbContext _context;
        public ProfileRepository(AnalysisDbContext context)
        {
            _context = context;
        }
       

        Tuple<List<Profile>, int> IProfileRepository.GetAllProfiles(int page, string search)
        {
            int pageSize = 1;
            var customerData = (from tempcustomer in _context.Profiles select tempcustomer);

            if (!string.IsNullOrEmpty(search))
            {
                customerData = customerData.Where(s => s.Tags.Contains(search.ToLower()) || s.Extension.Contains(search.ToLower()) || s.CallDuration.Value.Equals(search));
            }



            int recordsTotal = customerData.Count();
            var data = customerData.OrderByDescending(s => s.Creation).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Tuple.Create(data, recordsTotal);
        }
    }
}
