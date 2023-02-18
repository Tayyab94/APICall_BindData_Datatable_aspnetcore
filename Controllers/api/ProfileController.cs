using Application.Entiries.DataContext;
using Application.Entiries.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Application.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly AnalysisDbContext context;
        public ProfileController(AnalysisDbContext context)
        {
            this.context= context;
        }


        [HttpPost]
        public IActionResult GetProfiles()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData = (from tempcustomer in context.Profiles select tempcustomer);

                
                
               //List<ProfileViewModel> profiles = new List<ProfileViewModel>();

               // var query = context.Profiles.AsQueryable();

               
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(s => s.Id.ToString().Equals(searchValue));
                }

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                recordsTotal = customerData.Count();
                //var data = customerData.Skip(skip).Take(pageSize).Select(x => new ProfileViewModel
                //{
                //    Id = x.Id,
                //    Aggression = x.Aggression,

                //    Arousal = x.Arousal,
                //    Creation = x.Creation,
                //    Reference = x.Reference,

                //}).ToList();

                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
