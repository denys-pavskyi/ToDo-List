using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetAllTasksAsync();

        Task<TaskModel> GetTaskByIdAsync(int id);

        Task AddTaskAsync(TaskModel model);

        Task UpdateTaskAsync(TaskModel model);

        Task DeleteTaskAsync(int modelId);

        Task<bool> UpdateTaskStatusByIdAsync(int taskId, int statusId);
        Task<bool> UpdateTaskCategoryInTaskByIdAsync(int taskId, int categoryId);
    }
}
