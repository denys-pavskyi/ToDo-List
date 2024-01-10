using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITaskCategoryService
    {
        Task<IEnumerable<TaskCategoryModel>> GetAllTaskCategoriesAsync();

        Task<TaskCategoryModel> GetTaskCategoryByIdAsync(int id);

        Task AddTaskCategoryAsync(TaskCategoryModel model);

        Task UpdateTaskCategoryAsync(TaskCategoryModel model);

        Task DeleteTaskCategoryAsync(int modelId);
    }
}
