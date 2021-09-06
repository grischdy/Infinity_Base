using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infinity_Base.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infinity_Base.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Projekt> Projekte { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
