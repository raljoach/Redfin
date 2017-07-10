namespace Redfin.DataAccess.DB
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class RedfinContext : DbContext
    {
        // Your context has been configured to use a 'Redfin' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Redfin.DataAccess.DB.Redfin' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Redfin' 
        // connection string in the application configuration file.
        public RedfinContext()
            : base("name=Redfin")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Property> Properties { get; set; }
    }
}