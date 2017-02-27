using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridViewWebApi.Models
{
    public class GridViewModel
    {
        public GridViewModel(GridView theGridView)
        {
            GridTypeName = new Services.GridViewDataService().gridName(theGridView.GridTypeId);
            ViewName = theGridView.ViewName;
            ColumnLayout = theGridView.ColumnLayout;
            FilterDefinition = theGridView.FilterDefinition;
            IsDefault = theGridView.IsDefault;
            IsShared = theGridView.IsShared;
            UserID = theGridView.UserID;
        }
        public GridViewModel()
        {
        }
        public string GridTypeName { get; set; }
        public string ViewName { get; set; }
        public string ColumnLayout { get; set; }
        public string FilterDefinition { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsShared { get; set; }
        public int? UserID { get; set; }
    }
}