using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoListDbContext dbContext;
        private TaskRepository taskRepository;
        private TaskCategoryRepository taskCategoryRepository;


        public UnitOfWork(ToDoListDbContext context)
        {
            dbContext = context;
        }


        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public ITaskCategoryRepository TaskCategoryRepository
        {
            get
            {
                if (taskCategoryRepository == null)
                {
                    taskCategoryRepository = new TaskCategoryRepository(dbContext);
                }
                return taskCategoryRepository;
            }
        }

        public ITaskRepository TaskRepository
        {
            get
            {
                if (taskRepository == null)
                {
                    taskRepository = new TaskRepository(dbContext);
                }
                return taskRepository;
            }
        }

    }
}
