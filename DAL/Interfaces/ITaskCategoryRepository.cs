using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITaskCategoryRepository: IRepository<TaskCategory>
    {
        Task<IEnumerable<TaskCategory>> GetAllWithDetailsAsync();
        Task<TaskCategory> GetByIdWithDetailsAsync(int id);
    }
}
