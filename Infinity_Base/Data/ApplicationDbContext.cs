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
        public DbSet<SystemUnitClassLibLvl1> SystemUnitClassLibLvl1 { get; set; }
        public DbSet<SystemUnitClassLibLvl2> SystemUnitClassLibLvl2 { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
