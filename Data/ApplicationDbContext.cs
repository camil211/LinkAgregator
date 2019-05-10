using System;
using System.Collections.Generic;
using System.Text;
using LinkGen.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkGen.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserApplication>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }        

        public DbSet<Url> Urls { get; set; }

        public DbSet<Rate> Rate { get; set; }

        public DbSet<UserApplication> UsersApplication { get; set; }
    }
}
