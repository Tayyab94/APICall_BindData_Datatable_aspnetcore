using Application.Models.HelperModels;

namespace Application.Entiries.ViewModels
{
    public class ProfileListViewModel
    {
       public string searchValue { get; set; }
        public List<Profile> ProfileList { get; set; }
        public Pager Pager { get; set; }
    }
}
