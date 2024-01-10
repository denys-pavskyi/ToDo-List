using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = DAL.Entities.Task;

namespace DAL.Interfaces
{
    public interface ITaskRepository: IRepository<Task>
    {
        Task<IEnumerable<Task>> GetAllWithDetailsAsync();
        Task<Task> GetByIdWithDetailsAsync(int id);
        Task<Task> GetByIdWithNoTrackingAsync(int id);
    }
}
