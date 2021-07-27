using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Models.DataContext
{
    public class IntelCourseDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public IntelCourseDbContext(DbContextOptions<IntelCourseDbContext> options) : base(options)
        {

        }
    }
}
