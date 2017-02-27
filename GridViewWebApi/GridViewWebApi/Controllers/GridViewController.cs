using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GridViewWebApi.Services;
using GridViewWebApi.Models;
using System;

namespace GridViewWebApi.Controllers
{
    public class GridViewController : ApiController
    {
        GridViewDataService dataService = new GridViewDataService();


        /// <summary>
        /// Get data from URL such as http://localhost:53099/Api/GridView/1234/Customers
        /// </summary>
        [Route("api/GridView/{userId}/{gridTypeName}")]
        public HttpResponseMessage Get(int userId, string gridTypeName)
        {
            if (!dataService.validGridType(gridTypeName))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid grid type " + gridTypeName);
            var grids = dataService.getVisibleGridViews(userId, gridTypeName).ToList();
            var returnMe = grids.Select(x => new GridViewModel(x));
            return Request.CreateResponse(HttpStatusCode.OK, returnMe);
        }

        /// <summary>
        /// Post data to URL like http://localhost:53099/Api/GridView/2354/Vendors/Favorite%20Vendors <para />
        /// Use JSON body like {"ColumnLayout": "{some:json,goes:here}", "FilterDefinition": "{some:json,goes:here}", "IsDefault": true, "IsShared": false}<para />
        /// Note that if you specify a viewName or gridTypeName it will be ignored. If you specify a userId other than the one in the URL it will be forbidden.
        /// </summary>
        [Route("api/GridView/{userId}/{gridTypeName}/{viewName}")]
        public HttpResponseMessage Post(int userId, string gridTypeName, string viewName, GridViewModel theModel)
        {
            theModel.ViewName = viewName; //we ignore it if the user passed in a view name via GridViewModel
            theModel.GridTypeName = gridTypeName; //we ignore it if the user passed in a GridTypeName via GridViewModel
            if (theModel.UserID == null)
                theModel.UserID = userId;

            //Validation
            if (!dataService.hasAccess(userId, gridTypeName, viewName))
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "You can't modify views that belong to other people");
            if (!dataService.validGridType(gridTypeName))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid grid type " + gridTypeName);
            if (dataService.alreadyExists(theModel))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Grid already exists.");
            if (string.IsNullOrWhiteSpace(theModel.ColumnLayout)) 
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Column layout cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(theModel.FilterDefinition))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Filter definition cannot be null or whitespace.");
            if (theModel.IsDefault == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "\"IsDefault\" field cannot be null.");
            if (theModel.IsShared == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "\"IsShared\" field cannot be null.");

            //Database
            var returnMe = dataService.insert(theModel);
            return Request.CreateResponse(HttpStatusCode.OK, returnMe);
        }

        /// <summary>
        /// Delete from URL such as http://localhost:53099/Api/GridView/2354/Vendors/Favorite%20Vendors
        /// </summary>
        [Route("api/GridView/{userId}/{gridTypeName}/{viewName}")]
        public HttpResponseMessage Delete(int userId, string gridTypeName, string viewName)
        {
            if (!dataService.hasAccess(userId, gridTypeName, viewName))
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "You can't delete views that belong to other people");
            if (!dataService.validGridType(gridTypeName))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid grid type " + gridTypeName);
            if (!dataService.alreadyExists(gridTypeName, viewName)) 
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Grid does not exist.");
            var returnMe = dataService.deleteGridView(gridTypeName, viewName);
                return Request.CreateResponse(HttpStatusCode.OK, returnMe);
        }

        /// <summary>
        /// Modify data at URL like http://localhost:53099/Api/GridView/2354/Vendors/Favorite%20Vendors <para />
        /// Use JSON like { "ColumnLayout": "{new:json,goes:here}",}<para />
        /// Note that if you specify a viewName or gridTypeName it will be ignored. If you specify a userId other than the one in the URL it will be forbidden.
        /// </summary>
        [Route("api/GridView/{userId}/{gridTypeName}/{viewName}")]
        public HttpResponseMessage Patch(int userId, string gridTypeName, string viewName, GridViewModel theModel)
        {
            theModel.ViewName = viewName; //we ignore it if the user passed in a view name via GridViewModel
            theModel.GridTypeName = gridTypeName; //we ignore it if the user passed in a GridTypeName via GridViewModel
            if (theModel.UserID == null)
                theModel.UserID = userId;

            if (!dataService.hasAccess(userId, gridTypeName, viewName))
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "You can't modify views that belong to other people");
            if (!dataService.validGridType(gridTypeName))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid grid type " + gridTypeName);
            if (!dataService.alreadyExists(theModel))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Grid does not already exist.");


            var returnMe = dataService.insert(theModel);
            return Request.CreateResponse(HttpStatusCode.OK, returnMe);
        }

        /// <summary>
        /// Overwrite data at URL like http://localhost:53099/Api/GridView/2354/Vendors/Favorite%20Vendors <para />
        /// Use JSON like { "ColumnLayout": "{new:json,goes:here}",} <para />
        /// Note that if you specify a viewName or gridTypeName it will be ignored. If you specify a userId other than the one in the URL it will be forbidden.
        /// </summary>
        [Route("api/GridView/{userId}/{gridTypeName}/{viewName}")]
        public HttpResponseMessage Put(int userId, string gridTypeName, string viewName, GridViewModel theModel)
        {
            theModel.ViewName = viewName; //we ignore it if the user passed in a view name via GridViewModel
            theModel.GridTypeName = gridTypeName; //we ignore it if the user passed in a GridTypeName via GridViewModel
            if (theModel.UserID == null)
                theModel.UserID = userId;

            //Validation
            if (!dataService.hasAccess(userId, gridTypeName, viewName))
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "You can't modify views that belong to other people");
            if (!dataService.validGridType(gridTypeName))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid grid type " + gridTypeName);
            if (dataService.alreadyExists(theModel))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Grid already exists.");
            if (string.IsNullOrWhiteSpace(theModel.ColumnLayout))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Column layout cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(theModel.FilterDefinition))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Filter definition cannot be null or whitespace.");
            if (theModel.IsDefault == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "\"IsDefault\" field cannot be null.");
            if (theModel.IsShared == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "\"IsShared\" field cannot be null.");

            //Database
            var returnMe = dataService.insert(theModel);
            return Request.CreateResponse(HttpStatusCode.OK, returnMe);
        }
    }
}
