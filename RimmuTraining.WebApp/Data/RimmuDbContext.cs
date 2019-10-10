using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RimmuTraining.WebApp.Data
{
    public class RimmuDbContext : IdentityDbContext<RimmuUser, RimmuRole, Guid>
    {
        public RimmuDbContext(DbContextOptions<RimmuDbContext> options)
            : base(options)
        { }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
