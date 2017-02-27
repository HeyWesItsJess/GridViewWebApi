using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridViewWebApi.Models
{
    public class GridViewModel
    {
        private GridView theGridView;
        public GridViewModel(GridView theGridView)
        {
            this.theGridView = theGridView;
        }

        public string GridType
        {
            get
            {
                return theGridView.GridType.GridTypeName;
            }
        }
        public string ViewName
        {
            get
            {
                return theGridView.ViewName;
            }
        }

        public string ColumnLayout
        {
            get
            {
                return theGridView.ColumnLayout;
            }
        }
        public string FilterDefinition
        {
            get
            {
                return theGridView.FilterDefinition;
            }
        }

        public bool IsDefault
        {
            get
            {
                return theGridView.IsDefault;
            }
        }

        public bool IsShared
        {
            get
            {
                return theGridView.IsShared;
            }
        }

        public int UserID
        {
            get
            {
                return theGridView.UserID;
            }
        }
    }
}