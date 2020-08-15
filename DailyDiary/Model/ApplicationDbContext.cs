using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyDiary.Model
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base((options)) { }

        // Reference the Diary model, Contain it and Call it Diary
        public DbSet<Diary> Diary { get; set; }
    }
}
