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
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short GridTypeId { get; set; }

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
