using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Finals.Models;

namespace Finals.Data
{
    public class AppdbContext : DbContext
    {
        public AppdbContext (DbContextOptions<AppdbContext> options)
            : base(options)
        {
        }

        public DbSet<Finals.Models.Employee> Employee { get; set; }

        public DbSet<Finals.Models.Query> Query { get; set; }

      

        public DbSet<Finals.Models.Category> Category { get; set; }
      
    }
}
