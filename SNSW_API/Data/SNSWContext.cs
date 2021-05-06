using SNSW_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNSW_API.Data
{
    public class SNSWContext : DbContext
    {
        public SNSWContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<RegistrationDetail> RegistrationDetails { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Insurer> Insurers { get; set; }
        public DbSet<Registration> Registrations { get; set; }

    }
}
