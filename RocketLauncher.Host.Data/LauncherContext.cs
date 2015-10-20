using RocketLauncher.Host.Data.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RocketLauncher.Host.Data
{
    public class LauncherContext : DbContext
    {
        public LauncherContext()
            : base("LauncherContext")
        {
            
        }

        static LauncherContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LauncherContext>());
        }

        public DbSet<LauncherClient> LauncherClients { get; set; }

        public DbSet<LauncherSequence> LauncherSequences { get; set; }

        public DbSet<LauncherSequenceItem> LauncherSequenceItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
