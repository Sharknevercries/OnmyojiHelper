using Microsoft.EntityFrameworkCore;
using OnmyojiHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper
{
    public class OnmyojiContext : DbContext
    {
        public DbSet<Shikigami> Shikigamis { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Clue> Clues { get; set; }
        public DbSet<Bounty> Bounties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Onmyoji.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
