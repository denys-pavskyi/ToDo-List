using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DAL.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private readonly ToDoListDbContext _context;

        public TaskRepository(ToDoListDbContext toDoListDbContext)
        {
            _context = toDoListDbContext;
        }




        public async Task AddAsync(Entities.Task entity)
        {
            await _context.Tasks.AddAsync(entity);
        }

        public void Delete(Entities.Task entity)
        {
            _context.Tasks.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var Task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(Task);
        }

        public async Task<IEnumerable<Entities.Task>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }



        public async Task<Entities.Task> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public void Update(DAL.Entities.Task entity)
        {
            _context.Tasks.Update(entity);

        }

        public async Task<Entities.Task> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Tasks
                .Include(x => x.TaskCategory)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<IEnumerable<Entities.Task>> GetAllWithDetailsAsync()
        {
            return await _context.Tasks
                .Include(x => x.TaskCategory)
                .ToListAsync();
        }

    }
}
