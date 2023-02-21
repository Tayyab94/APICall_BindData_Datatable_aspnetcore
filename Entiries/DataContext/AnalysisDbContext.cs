using Microsoft.EntityFrameworkCore;

namespace Application.Entiries.DataContext
{
    public class AnalysisDbContext:DbContext
    {
        public AnalysisDbContext(DbContextOptions<AnalysisDbContext> options)
             : base(options)
        {
        }

      public DbSet<Profile> Profiles { get; set; }  
        public DbSet<Segment> Segments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);

        }
    }
}
