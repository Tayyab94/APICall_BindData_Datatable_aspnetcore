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


        public async Task<ActionResult> AllProfilesList()
        {
            var data = await _profileRepository.ExtensionsList();
            ViewBag.extensionList = data;
            return View();
        }

        public async Task<ActionResult> ListofProfiles()
        {
            var entensionsList = await _profileRepository.ExtensionsList();
            ViewBag.extensionList = entensionsList;
            var data =await _profileRepository.GetProfiles();
            
            return View(data);  
        }

    }
}