using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpListScheduler
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\PRIVATE_AREA\PRYWATNE_PROJEKTY_C_SHARP\repos\ImpListApp\ImpListApp.db");
    }
}
