using Application.Entiries.DataContext;
using Application.Entiries.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Application.Models.QueryParamModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            string[] newSearches=name.Split(',');
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (!string.IsNullOrEmpty(dt))
            {
                string[] dates = dt.Split(' ');
                startDate = Convert.ToDateTime(dates[0]).Date;
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

                //if (!string.IsNullOrEmpty(name))
                //{
                //    customerData = customerData.Where(s => s.Tags.Contains(name.ToLower()) || s.Extension.Contains(name.ToLower()) || s.CallDuration.Value.Equals(name)).Where(s => s.Creation.Date >= startDate.Date && s.Creation.Date <= endDate.Date).AsQueryable();

                //}

                //if (!string.IsNullOrEmpty(newSearches[0]) || !string.IsNullOrEmpty(newSearches[1]) || !string.IsNullOrEmpty(newSearches[2]))
                //{
                //    customerData = customerData.Where(s => (s.Tags.ToLower().Contains(newSearches[0].ToLower()) 
                //                         && s.Extension.ToLower().Contains(newSearches[1].ToLower()) &&
                //    (
                //    s.CallSource.ToLower().Contains(newSearches[2].ToLower()) ||
                //    s.CallDestination.ToLower().Contains(newSearches[2].ToLower())))).Where(s => s.Creation.Date >= startDate.Date && s.Creation.Date <= endDate.Date).AsQueryable();

                //}

                //if (!string.IsNullOrEmpty(newSearches[0]) || !string.IsNullOrEmpty(newSearches[1]) || !string.IsNullOrEmpty(newSearches[2]))
                //{
                //    customerData = customerData.Where(s => newSearches[0].ToLower().Contains(s.Tags.ToLower()) && newSearches[1].ToLower().Contains(s.Extension.ToLower()) &&
                //    (s.CallDuration.Value.Equals(Convert.ToDouble(newSearches[2])) || newSearches[2].ToLower().Contains(s.CallSource.ToLower()) || newSearches[2].ToLower().Contains(s.CallDestination.ToLower()))).Where(s => s.Creation.Date >= startDate.Date && s.Creation.Date <= endDate.Date).AsQueryable();

                //}

                if (!string.IsNullOrEmpty(newSearches[0]))
                {
                    customerData = customerData.Where(s => newSearches[0].ToLower().Contains(s.Tags.ToLower())).AsQueryable();

                }
                if (!string.IsNullOrEmpty(newSearches[1]))
                {
                    customerData = customerData.Where(s => newSearches[1].ToLower().Contains(s.Extension.ToLower())).AsQueryable();

                }
                //if (!string.IsNullOrEmpty(newSearches[2]))
                //{
                //    customerData = customerData.Where(s => (newSearches[2].ToLower().Contains(s.CallSource.ToLower()) || newSearches[2].ToLower().Contains(s.CallDestination.ToLower()))).Where(s => s.Creation.Date >= startDate.Date && s.Creation.Date <= endDate.Date).AsQueryable();

                //}
                if (!string.IsNullOrEmpty(newSearches[2]))
                {
                    customerData = customerData.Where(s => (newSearches[2].ToLower().Contains(s.CallSource.ToLower()) || newSearches[2].ToLower().Contains(s.CallDestination.ToLower()))).AsQueryable();

                }
                if (!string.IsNullOrEmpty(newSearches[3]))
                {
                    customerData = customerData.Where(s => s.CallDuration.Value.Equals(double.Parse(newSearches[3]))).AsQueryable();

                }

                if (startDate != endDate)
                {
                    customerData = customerData.Where(s => s.Creation.Date >= startDate.Date && s.Creation.Date <= endDate.Date).AsQueryable();
                }
                else
                {
                    customerData = customerData.Where(s => s.Creation.Date >= DateTime.Now.Date && s.Creation.Date <= DateTime.Now.Date).AsQueryable();
                }

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection).AsQueryable();
                }
                else
                {
                    customerData = customerData.OrderByDescending(s => s.Creation).AsQueryable();
                }

                recordsTotal = customerData.ToList().Count();

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
        public IActionResult GetProfilesAll([FromQuery] string name, string dt)
        {
            string[] newSearches = name.Split(',');
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (!string.IsNullOrEmpty(dt))
            {
                string[] dates = dt.Split(' ');
                startDate = Convert.ToDateTime(dates[0]).Date;
                endDate = Convert.ToDateTime(dates[1]).Date;
            }

            try
            {
                var customerData = (from tempcustomer in context.Profiles select tempcustomer).AsQueryable();

                if (!string.IsNullOrEmpty(newSearches[0]))
                {
                    customerData = customerData.Where(s => newSearches[0].ToLower().Contains(s.Tags.ToLower())).AsQueryable();

                }
                if (!string.IsNullOrEmpty(newSearches[1]))
                {
                    customerData = customerData.Where(s => newSearches[1].ToLower().Contains(s.Extension.ToLower())).AsQueryable();

                }
                if (!string.IsNullOrEmpty(newSearches[2]))
                {
                    customerData = customerData.Where(s => (newSearches[2].ToLower().Contains(s.CallSource.ToLower()) || newSearches[2].ToLower().Contains(s.CallDestination.ToLower()))).AsQueryable();

                }
                if (!string.IsNullOrEmpty(newSearches[3]))
                {
                    customerData = customerData.Where(s => s.CallDuration.Value.Equals(double.Parse(newSearches[3]))).AsQueryable();

                }

                if (startDate != endDate)
                {
                    customerData = customerData.Where(s => s.Creation.Date >= startDate.Date && s.Creation.Date <= endDate.Date).AsQueryable();
                }
                else
                {
                    customerData = customerData.Where(s => s.Creation.Date >= DateTime.Now.Date && s.Creation.Date <= DateTime.Now.Date).AsQueryable();
                }

                    customerData = customerData.OrderByDescending(s => s.Creation).AsQueryable();
                

                var data = customerData.ToList();

                return Ok(new { record=data});
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //[HttpPost]
        //[Consumes("application/json")]
        //public IActionResult GetProfiles([FromBody] ProfileQueryViewModel model)
        //{
        //    DateTime startDate = DateTime.Now;
        //    DateTime endDate = DateTime.Now;
        //    if (!string.IsNullOrEmpty(model.Dt))
        //    {
        //        string[] dates = model.Dt.Split(' ');
        //         startDate = Convert.ToDateTime(dates[0]).Date.AddDays(1);
        //         endDate = Convert.ToDateTime(dates[1]).Date;
        //    }

        //    try
        //    {
        //        var draw = Request.Form["draw"].FirstOrDefault();
        //        var start = Request.Form["start"].FirstOrDefault();
        //        var length = Request.Form["length"].FirstOrDefault();
        //        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int recordsTotal = 0;
        //        var customerData = (from tempcustomer in context.Profiles select tempcustomer).AsQueryable();



        //        //List<ProfileViewModel> profiles = new List<ProfileViewModel>();

        //        // var query = context.Profiles.AsQueryable();
        //        //if (!string.IsNullOrEmpty(dt))
        //        //{
        //        //    customerData = customerData.Where(s => (s.Creation.Date <= startDate && s.Creation.Date >= endDate)).AsQueryable();
        //        //}

        //        if (!string.IsNullOrEmpty(model.Name))
        //        {
        //            customerData = customerData.Where(s => s.Tags.Contains(model.Name.ToLower()) || s.Extension.Contains(model.Name.ToLower()) || s.CallDuration.Value.Equals(model.Name)).Where(s=>s.Creation.Date>= startDate.Date && s.Creation.Date<=endDate.Date).AsQueryable();

        //        }
        //        //if (!string.IsNullOrEmpty(name))
        //        //{
        //        //    customerData = customerData.Where(s =>  s.Tags.ToLower().Contains(name.ToLower()) || s.Extension.ToLower().Contains(name.ToLower()) ||
        //        //                        s.CallDuration.Value.Equals(name) ||( s.CallSource.ToLower().Equals(name.ToLower()) && !string.IsNullOrEmpty(s.CallSource)) 
        //        //                        || (s.CallDestination.ToLower().Equals(name.ToLower())&& !string.IsNullOrEmpty(s.CallDestination)));

        //        //}



        //        //if (!string.IsNullOrEmpty(searchValue))
        //        //{
        //        //    customerData = customerData.Where(s => s.Tags.Contains(searchValue.ToLower()) || s.Extension.Contains(searchValue.ToLower()) || s.CallDuration.Value.Equals(searchValue)|| s.CallSource.ToLower().Contains(searchValue.ToLower()) || s.CallDestination.ToLower().Contains(searchValue.ToLower()));
        //        //}

        //        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        //        {
        //            customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection).AsQueryable();
        //        }
        //        else
        //        {
        //            customerData = customerData.OrderByDescending(s => s.Creation).AsQueryable();
        //        }

        //        recordsTotal = customerData.Count();

        //        var data = customerData.Skip(skip).Take(pageSize).ToList();
        //        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        //        return Ok(jsonData);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public IActionResult GetProfiles([FromQuery] string name, string dt, string tags, string extensions)
        //{
        //    DateTime startDate = DateTime.Now;
        //    DateTime endDate = DateTime.Now;
        //    if (!string.IsNullOrEmpty(dt))
        //    {
        //        string[] dates = dt.Split(' ');
        //        startDate = Convert.ToDateTime(dates[0]).Date.AddDays(1);
        //        endDate = Convert.ToDateTime(dates[1]).Date;
        //    }

        //    try
        //    {
        //        var draw = Request.Form["draw"].FirstOrDefault();
        //        var start = Request.Form["start"].FirstOrDefault();
        //        var length = Request.Form["length"].FirstOrDefault();
        //        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int recordsTotal = 0;
        //        var customerData = (from tempcustomer in context.Profiles select tempcustomer).AsQueryable();



        //        //List<ProfileViewModel> profiles = new List<ProfileViewModel>();

        //        // var query = context.Profiles.AsQueryable();
        //        //if (!string.IsNullOrEmpty(dt))
        //        //{
        //        //    customerData = customerData.Where(s => (s.Creation.Date <= startDate && s.Creation.Date >= endDate)).AsQueryable();
        //        //}

        //        if (!string.IsNullOrEmpty(name))
        //        {
        //            customerData = customerData.Where(s => s.Tags.Contains(name.ToLower()) || s.Extension.Contains(name.ToLower()) || s.CallDuration.Value.Equals(name)).Where(s => s.Creation.Date >= startDate.Date && s.Creation.Date <= endDate.Date).AsQueryable();

        //        }
        //        //if (!string.IsNullOrEmpty(name))
        //        //{
        //        //    customerData = customerData.Where(s =>  s.Tags.ToLower().Contains(name.ToLower()) || s.Extension.ToLower().Contains(name.ToLower()) ||
        //        //                        s.CallDuration.Value.Equals(name) ||( s.CallSource.ToLower().Equals(name.ToLower()) && !string.IsNullOrEmpty(s.CallSource)) 
        //        //                        || (s.CallDestination.ToLower().Equals(name.ToLower())&& !string.IsNullOrEmpty(s.CallDestination)));

        //        //}



        //        //if (!string.IsNullOrEmpty(searchValue))
        //        //{
        //        //    customerData = customerData.Where(s => s.Tags.Contains(searchValue.ToLower()) || s.Extension.Contains(searchValue.ToLower()) || s.CallDuration.Value.Equals(searchValue)|| s.CallSource.ToLower().Contains(searchValue.ToLower()) || s.CallDestination.ToLower().Contains(searchValue.ToLower()));
        //        //}

        //        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        //        {
        //            customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection).AsQueryable();
        //        }
        //        else
        //        {
        //            customerData = customerData.OrderByDescending(s => s.Creation).AsQueryable();
        //        }

        //        recordsTotal = customerData.Count();

        //        var data = customerData.Skip(skip).Take(pageSize).ToList();
        //        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        //        return Ok(jsonData);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

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
