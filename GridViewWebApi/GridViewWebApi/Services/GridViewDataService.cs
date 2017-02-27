using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridViewWebApi.Services
{
    public class GridViewDataService
    {
        GridViewsDataContext gridViewsDataContext = new GridViewsDataContext();

        public IEnumerable<GridView> getGridViewsByUserIdAndType (int userId, string gridType)
        {
            return gridViewsDataContext.GridViews
                .Where(x => (x.UserID == userId || x.IsShared) && x.GridType.GridTypeName == gridType)
                .ToList();
        }
    }
}