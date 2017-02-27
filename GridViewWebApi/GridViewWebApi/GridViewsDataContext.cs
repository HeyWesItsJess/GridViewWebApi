namespace GridViewWebApi
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GridViewsDataContext : DbContext
    {
        public GridViewsDataContext()
            : base("name=GridViewsDataModel")
        {
        }

        public virtual DbSet<GridType> GridTypes { get; set; }
        public virtual DbSet<GridView> GridViews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
