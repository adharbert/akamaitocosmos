using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{

    public class AkamaiContext : DbContext
    {

        public AkamaiContext(DbContextOptions<AkamaiContext> options) : base(options) 
        {  }

        public DbSet<AkamaiUser> AkamaiUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Database level configuration
            modelBuilder.HasManualThroughput(600);

            // Entity Level Configuration
            modelBuilder.Entity<AkamaiUser>()
                .ToContainer("AkamaiUser")
                .HasNoDiscriminator()
                .HasPartitionKey(user => user.Uuid)
                .HasKey(user => user.Id)
                ;


            modelBuilder.Entity<AkamaiUser>()
                .Property(user => user.CustomETag)
                .IsETagConcurrency();


        }


    }
}
