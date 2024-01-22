using Microsoft.EntityFrameworkCore;
using Toscan_Insaat_Final.Models;

namespace Toscan_Insaat_Final.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }  
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<Slider> Slides { get; set; }
        public DbSet<SocialAccount> SocialAccounts { get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<About>().Property(x => x.Title).IsRequired();
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
