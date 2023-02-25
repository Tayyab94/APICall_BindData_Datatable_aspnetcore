using System.Diagnostics;
using Application.Entiries;
using Application.Entiries.DataContext;
using Application.Entiries.DataTable;
using Application.Entiries.ViewModels;
using Application.Models;
using Application.Models.HelperModels;
using Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly AnalysisDbContext context;
        private readonly IProfileRepository _profileRepository;
        public HomeController(ILogger<HomeController> logger, AnalysisDbContext context,IProfileRepository profileRepository)
        {
            //_logger = logger;
            //this.context = context;
            _profileRepository = profileRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Controller Datatable call, Dont remove
        //public ActionResult GetAllProfiles(JqueryDatatableParam param)
        //{
        //    int totalCount = 0; int pageNo = 1;
        //    if (param.iDisplayStart >= param.iDisplayLength)
        //        pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;
        //    //List<ProfileViewModel> profiles = new List<ProfileViewModel>();
        //    List<Profile> profiles = new List<Profile>();

        //    var query = context.Profiles.AsQueryable();
        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {


        //        query = query.Where(s => s.Id.ToString().Contains(param.sSearch)
        //                  ).AsQueryable();
        //        totalCount = query.Count();
        //    }
        //    else
        //    {
        //        totalCount = query.Count();

        //    }

        //    var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"].ToString()); /*Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);*/
        //    var sortDirection = HttpContext.Request.Query["sSortDir_0"].ToString(); /*HttpContext.Request.QueryString["sSortDir_0"];*/

        //    if (sortColumnIndex == 0)
        //    {
        //        query = sortDirection == "asc" ? query.OrderBy(c => c.Arousal) : query.OrderByDescending(c => c.Arousal);
        //    }
        //    else if (sortColumnIndex == 1)
        //    {
        //        query = sortDirection == "asc" ? query.OrderBy(c => c.Reference) : query.OrderByDescending(c => c.Reference);
        //    }
        //    else
        //    {
        //        query = sortDirection == "asc" ? query.OrderBy(c => c.Aggression) : query.OrderByDescending(c => c.Aggression);
        //    }


        //    //profiles = query.Skip(param.iDisplayStart)
        //    //             .Take(param.iDisplayLength).Select(x => new ProfileViewModel
        //    //             {
        //    //                 Id = x.Id,
        //    //                 Aggression = x.Aggression,

        //    //                 Arousal = x.Arousal,
        //    //                 Creation = x.Creation,
        //    //                 Reference = x.Reference,

        //    //             }).ToList();

        //    profiles = query.Skip(param.iDisplayStart)
        //               .Take(param.iDisplayLength).ToList();
        //    return Json(new
        //    {
        //        param.sEcho,
        //        iTotalRecords = totalCount,
        //        iTotalDisplayRecords = totalCount,
        //        aaData = profiles
        //    });

        //}

        #endregion

        #region Code-25-02-2023 

        public async Task<ActionResult> UserProfile()
        {
            var data = await _profileRepository.ExtensionsList();
            ViewBag.extensionList = data;
            return View();
        }
        #endregion



        public ActionResult ProfileS()
        {

            return View();
        }

        public async Task<ActionResult> AllProfilesList()
        {
            var data = await _profileRepository.ExtensionsList();
            ViewBag.extensionList = data;
            return View();
        }


        public PartialViewResult ListofProfiles(int? pageNo, string search)
        {
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            ProfileListViewModel model = new ProfileListViewModel();
            model.searchValue = search;
            var data = _profileRepository.GetAllProfiles(pageNo.Value, search);
            model.ProfileList = data.Item1;

            if (model.ProfileList != null)
            {
                model.Pager = new Pager(data.Item2, pageNo, 1);
                return PartialView("_ListofProdiles", model);
            }
            return PartialView("_ListofProdiles", model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}