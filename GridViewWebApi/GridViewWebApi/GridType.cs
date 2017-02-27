namespace GridViewWebApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GridType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short GridTypeId { get; set; }

        [Required]
        public string GridTypeName { get; set; }
    }
}
