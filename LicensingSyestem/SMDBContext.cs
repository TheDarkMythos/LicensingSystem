using LicensingSyestem.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LicensingSyestem
{
    public class SMDBContext : DbContext
    {
        public SMDBContext(DbContextOptions<SMDBContext> options) : base(options)
        {
        }
        public DbSet<Application> Applications { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Client> Clients { get; set; }

    }
}
