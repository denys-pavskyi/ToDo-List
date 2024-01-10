using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace DAL.Repositories
{
    public class TaskCategoryRepository: ITaskCategoryRepository
    {
        private readonly ToDoListDbContext _context;

        public TaskCategoryRepository(ToDoListDbContext toDoListDbContext)
        {
            _context = toDoListDbContext;
        }

        public async Task AddAsync(TaskCategory entity)
        {
            await _context.TaskCategories.AddAsync(entity);
        }

        public void Delete(TaskCategory entity)
        {
            _context.TaskCategories.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var Task = await _context.TaskCategories.FindAsync(id);
            _context.TaskCategories.Remove(Task);
        }

        public async Task<IEnumerable<TaskCategory>> GetAllAsync()
        {
            return await _context.TaskCategories.ToListAsync();
        }

        public async Task<TaskCategory> GetByIdAsync(int id)
        {
            return await _context.TaskCategories.FindAsync(id);
        }

        public void Update(TaskCategory entity)
        {
            _context.TaskCategories.Update(entity);

        }

        public async Task<TaskCategory> GetByIdWithDetailsAsync(int id)
        {
            return await _context.TaskCategories
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<TaskCategory>> GetAllWithDetailsAsync()
        {
            return await _context.TaskCategories
                .Include(x => x.Tasks)
                .ToListAsync();
        }
 
    }
}
