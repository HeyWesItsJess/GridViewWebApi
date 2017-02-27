using System;
using System.Collections.Generic;
using System.Linq;
using GridViewWebApi.Models;

namespace GridViewWebApi.Services
{
    public class GridViewDataService
    {
        GridViewsDataContext gridViewsDataContext = new GridViewsDataContext();
        public GridViewDataService()
        {
            gridViewsDataContext.Database.Connection.Open();
        }

        public IEnumerable<GridView> getVisibleGridViews(int userId, string gridType)
        {
            return gridViewsDataContext.GridViews
                .Where(x => (x.UserID == userId || x.IsShared) && x.GridType.GridTypeName == gridType)
                .ToList();
        }
        public GridViewModel deleteGridView(string gridTypeName, string viewName)
        {
            GridView deleteMe = gridViewsDataContext.GridViews
                .Where(x => x.GridType.GridTypeName == gridTypeName && x.ViewName == viewName).Single();
            GridViewModel returnMe = new GridViewModel(deleteMe);
            gridViewsDataContext.GridViews.Remove(deleteMe);
            gridViewsDataContext.SaveChanges();
            return returnMe;
        }
        public bool validGridType(string gridType)
        {
            return gridViewsDataContext.GridTypes
                .Where(x => x.GridTypeName == gridType)
                .Any();
        }
        public short GridTypeId(string gridType)
        {
            return gridViewsDataContext.GridTypes
                .Where(x => x.GridTypeName == gridType)
                .Select(x => x.GridTypeId)
                .Single();
        }

        internal bool hasAccess(int userId, string gridTypeName, string viewName)
        {
            GridView deleteMe = gridViewsDataContext.GridViews
                .Where(x => x.GridType.GridTypeName == gridTypeName && x.ViewName == viewName).SingleOrDefault();
            return deleteMe == null || deleteMe.UserID == userId;
        }

        public GridViewModel insert(GridViewModel thePatch)
        {
            if (alreadyExists(thePatch))
            {
                var original = getOriginal(thePatch);
                original.applyModel(thePatch);
            }
            else
                gridViewsDataContext.GridViews.Add(new GridView(thePatch));
            gridViewsDataContext.SaveChanges();
            return new GridViewModel(getOriginal(thePatch));
        }
        public GridView getOriginal(GridViewModel theModel)
        {
            return gridViewsDataContext.GridViews
            .Where(x => x.GridType.GridTypeName == theModel.GridTypeName && x.ViewName == theModel.ViewName)
            .First();
        }

        public bool alreadyExists(GridViewModel checkMe)
        {
            return alreadyExists(checkMe.GridTypeName, checkMe.ViewName);
        }
        public bool alreadyExists(string GridTypeName, string ViewName)
        {
            var possibleMatches = gridViewsDataContext.GridViews
                .Where(x => x.GridType.GridTypeName == GridTypeName && x.ViewName == ViewName);
            return possibleMatches.Any();
        }

        public string gridName(short? id)
        {
            return gridViewsDataContext.GridTypes.Where(x => x.GridTypeId == id).Select(x => x.GridTypeName).SingleOrDefault();
        }
    }
}