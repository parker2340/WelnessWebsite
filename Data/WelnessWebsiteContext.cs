using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WelnessWebsite.Models;

namespace WelnessWebsite.Data
{
    public class WelnessWebsiteContext : DbContext
    {
        public WelnessWebsiteContext (DbContextOptions<WelnessWebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<WelnessWebsite.Models.User> User { get; set; } = default!;

        public DbSet<WelnessWebsite.Models.Workout>? Workout { get; set; }

        public DbSet<WelnessWebsite.Models.DailyWorkout>? DailyWorkout { get; set; }
    }
}
