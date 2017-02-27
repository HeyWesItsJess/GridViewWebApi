using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GridViewWebApi.Services;
using GridViewWebApi.Models;

namespace GridViewWebApi.Controllers
{
    public class GridViewController : ApiController
    {
        GridViewDataService dataService = new GridViewDataService();

        [Route("api/GridView/{userId}/{gridType}")]
        public IEnumerable<GridViewModel> Get(int userId, string gridType)
        {
            try
            {
                var returnMe = dataService.getGridViewsByUserIdAndType(userId, gridType).ToList();
                return returnMe.Select(x => new GridViewModel(x));
            }
            catch
            {
                throw;// new System.Web.HttpException(404, "There was a problem determining the gridType meant by \"" + gridType + "\".");
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
