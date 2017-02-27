namespace GridViewWebApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GridView")]
    public partial class GridView
    {
        public GridView()
        {
        }
        public GridView(Models.GridViewModel createFromMe)
        {
            applyModel(createFromMe);
        }

        public void applyModel(Models.GridViewModel createFromMe)
        {
            if (!string.IsNullOrEmpty(createFromMe.GridTypeName))
                    GridTypeId = new Services.GridViewDataService().GridTypeId(createFromMe.GridTypeName);
            if (!string.IsNullOrEmpty(createFromMe.ViewName))
                ViewName = createFromMe.ViewName;
            if (!string.IsNullOrEmpty(createFromMe.ColumnLayout))
                ColumnLayout = createFromMe.ColumnLayout;
            if (!string.IsNullOrEmpty(createFromMe.FilterDefinition))
                FilterDefinition = createFromMe.FilterDefinition;
            if (createFromMe.IsDefault != null)
                IsDefault = (bool)createFromMe.IsDefault;
            if (createFromMe.IsShared != null)
                IsShared = (bool)createFromMe.IsShared;
            if (createFromMe.UserID != null)
                UserID = (int)createFromMe.UserID;
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short? GridTypeId { get; set; } = null;

        [Key]
        [Column(Order = 1)]
        [StringLength(256)]
        public string ViewName { get; set; }

        [Required]
        public string ColumnLayout { get; set; }

        [Required]
        public string FilterDefinition { get; set; }

        public bool IsDefault { get; set; }

        public bool IsShared { get; set; }

        public int UserID { get; set; }

        public virtual GridType GridType { get; set; }
    }
}
