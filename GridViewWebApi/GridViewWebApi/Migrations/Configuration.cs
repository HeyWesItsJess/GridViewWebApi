namespace GridViewWebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GridViewWebApi.GridViewsDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "GridViewWebApi.GridViewsDataContext";
        }

        protected override void Seed(GridViewsDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.GridTypes.AddOrUpdate(x => x.GridTypeId,
                new GridType() { GridTypeId = 0, GridTypeName = "Customers" },
                new GridType() { GridTypeId = 1, GridTypeName = "Orders" },
                new GridType() { GridTypeId = 2, GridTypeName = "Vendors" }
                );

            context.GridViews.AddOrUpdate(x => x.ViewName,
                new GridView()
                {
                    GridTypeId = 0,
                    ViewName = "Crazy Customers",
                    ColumnLayout = "{some:json,goes:here}",
                    FilterDefinition = "{some:json,goes:here}",
                    IsDefault = true,
                    IsShared = false,
                    UserID = 1234
                }, new GridView()
                {
                    GridTypeId = 1,
                    ViewName = "Obligatory Orders",
                    ColumnLayout = "{some:json,goes:here}",
                    FilterDefinition = "{some:json,goes:here}",
                    IsDefault = false,
                    IsShared = true,
                    UserID = 1234
                }, new GridView()
                {
                    GridTypeId = 2,
                    ViewName = "Various Vendors",
                    ColumnLayout = "{some:json,goes:here}",
                    FilterDefinition = "{some:json,goes:here}",
                    IsDefault = true,
                    IsShared = false,
                    UserID = 2345
                }
                );

            context.SaveChanges();
        }
    }
}
