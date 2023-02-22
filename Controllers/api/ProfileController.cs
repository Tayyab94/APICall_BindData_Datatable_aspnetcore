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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly AnalysisDbContext context;
        public ProfileController(AnalysisDbContext context)
        {
            this.context= context;
        }


        [HttpPost]
        public IActionResult GetProfiles([FromQuery] string name, string dt)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (!string.IsNullOrEmpty(dt))
            {
                string[] dates = dt.Split(' ');
                 startDate = Convert.ToDateTime(dates[0]).Date.AddDays(1);
                 endDate = Convert.ToDateTime(dates[1]).Date;
            }
          
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
                var customerData = (from tempcustomer in context.Profiles select tempcustomer).AsQueryable();



                //List<ProfileViewModel> profiles = new List<ProfileViewModel>();

                // var query = context.Profiles.AsQueryable();
                //if (!string.IsNullOrEmpty(dt))
                //{
                //    customerData = customerData.Where(s => (s.Creation.Date <= startDate && s.Creation.Date >= endDate)).AsQueryable();
                //}

                if (!string.IsNullOrEmpty(name))
                {
                    customerData = customerData.Where(s => s.Tags.Contains(name.ToLower()) || s.Extension.Contains(name.ToLower()) || s.CallDuration.Value.Equals(name)).Where(s=>s.Creation.Date>= startDate.Date && s.Creation.Date<=endDate.Date).AsQueryable();

                }
                //if (!string.IsNullOrEmpty(name))
                //{
                //    customerData = customerData.Where(s =>  s.Tags.ToLower().Contains(name.ToLower()) || s.Extension.ToLower().Contains(name.ToLower()) ||
                //                        s.CallDuration.Value.Equals(name) ||( s.CallSource.ToLower().Equals(name.ToLower()) && !string.IsNullOrEmpty(s.CallSource)) 
                //                        || (s.CallDestination.ToLower().Equals(name.ToLower())&& !string.IsNullOrEmpty(s.CallDestination)));
                    
                //}

             

                //if (!string.IsNullOrEmpty(searchValue))
                //{
                //    customerData = customerData.Where(s => s.Tags.Contains(searchValue.ToLower()) || s.Extension.Contains(searchValue.ToLower()) || s.CallDuration.Value.Equals(searchValue)|| s.CallSource.ToLower().Contains(searchValue.ToLower()) || s.CallDestination.ToLower().Contains(searchValue.ToLower()));
                //}

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection).AsQueryable();
                }
                else
                {
                    customerData = customerData.OrderByDescending(s => s.Creation).AsQueryable();
                }

                recordsTotal = customerData.Count();
              
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult GetProfiles1()
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
                    customerData = customerData.Where(s => s.Id.ToString().Equals(searchValue) || s.Tags.Contains(searchValue.ToLower()) || s.Extension.Contains(searchValue.ToLower()));
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

                var data = customerData.OrderByDescending(s => s.Creation).Skip(skip).Take(pageSize).ToList();
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
