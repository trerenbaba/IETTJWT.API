using IETTJWT.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IETTJWT.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {

        public DbSet<UserRefreshToken> userRefreshTokens { get; set; }




        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<UserRefreshToken>().HasKey(x => x.UserId);
            builder.Entity<UserRefreshToken>().Property(x => x.UserId).ValueGeneratedNever();

            base.OnModelCreating(builder);
        }

    }
}
