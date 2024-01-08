using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITaskService: ICrud<TaskModel>
    {
        Task<bool> UpdateTaskStatusByIdAsync(int taskId, int statusId);
        Task<bool> UpdateTaskCategoryByIdAsync(int taskId, int categoryId);
    }
}
