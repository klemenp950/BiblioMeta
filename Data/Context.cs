using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BiblioMeta.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BiblioMeta.Data
{
    public class Context : IdentityDbContext<IdentityUser>
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<BiblioMeta.Models.Knjiga> Knjiga { get; set; } = default!;
        public DbSet<BiblioMeta.Models.Avtor> Avtor { get; set; } = default!;
        public DbSet<BiblioMeta.Models.Zanr> Zanr { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }
    }
}
