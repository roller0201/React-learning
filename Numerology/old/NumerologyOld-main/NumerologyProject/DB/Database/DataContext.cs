using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Database.AuthDomain;
using Numerology.Domain.Models;

namespace Database
{
    public class DataContext : IdentityDbContext<AppUser, AppUserRole, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public string CurrentUserId { get; set; }

        public DbSet<NameModel> NameModels { get; set; }
        public DbSet<ClientModel> ClientModels { get; set; }
        public DbSet<LetterModel> LetterModels { get; set; }
        public DbSet<NumerologyPortraitModel> NumerologyPortraitModels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AppUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUserRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AppUserRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
