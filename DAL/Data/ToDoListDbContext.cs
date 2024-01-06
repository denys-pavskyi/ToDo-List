using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Task = DAL.Entities.Task;

namespace DAL.Data
{
    public class ToDoListDbContext: DbContext
    {

        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options): base(options) {
            Database.EnsureCreated();
        }


        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne(x => x.TaskCategory).WithMany(x => x.Tasks).HasForeignKey(x => x.TaskCategoryId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskCategory>()
                .HasMany(x => x.Tasks).WithOne(x => x.TaskCategory).HasForeignKey(x => x.TaskCategoryId).OnDelete(DeleteBehavior.NoAction);


            SeedData(modelBuilder);

        }


        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskCategory>().HasData(
                    new TaskCategory { Id = 1, Name = "Work" },
                    new TaskCategory { Id = 2, Name = "Finances" },
                    new TaskCategory { Id = 3, Name = "Education" },
                    new TaskCategory { Id = 4, Name = "Home" }
            );

            modelBuilder.Entity<Task>().HasData(
                new Task { Id = 1, Name = "Complete project proposal", TaskCategoryId = 1, Status = Status.InProgress },
                new Task { Id = 2, Name = "Schedule meetings for the team", TaskCategoryId = 1, Status = Status.Backlog },
                new Task { Id = 3, Name = "Prepare presentation for the client meeting", TaskCategoryId = 1, Status = Status.Backlog },

                new Task { Id = 4, Name = "Budget planning for the month", TaskCategoryId = 2, Status = Status.Backlog },
                new Task { Id = 5, Name = "Pay bills", TaskCategoryId = 2, Status = Status.InProgress },
                new Task { Id = 6, Name = "Review and update investments", TaskCategoryId = 2, Status = Status.Backlog },

                new Task { Id = 7, Name = "Study for upcoming exam", TaskCategoryId = 3, Status = Status.Backlog },
                new Task { Id = 8, Name = "Complete an online course", TaskCategoryId = 3, Status = Status.InProgress },
                new Task { Id = 9, Name = "Research for a project", TaskCategoryId = 3, Status = Status.Backlog },

                new Task { Id = 10, Name = "Clean and organize the living room", TaskCategoryId = 4, Status = Status.Complete },
                new Task { Id = 11, Name = "Grocery shopping", TaskCategoryId = 4, Status = Status.Backlog },
                new Task { Id = 12, Name = "Plan home maintenance tasks", TaskCategoryId = 4, Status = Status.Backlog }
            );


        }

    }
}
