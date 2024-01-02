using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ToDoListDbContext: DbContext
    {

        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options): base(options) {
            Database.EnsureCreated();
        }


        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }


        private void SeedData(ModelBuilder modelBuilder)
        {
            //ToDo
        }

    }
}
