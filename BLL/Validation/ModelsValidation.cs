using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public static class ModelsValidation
    {

        public static void TaskModelValidation(TaskModel model)
        {
            if (model == null)
            {
                throw new ToDoListException("Task was null");
            }

            if (model.TaskCategoryId < 0)
            {
                throw new ToDoListException("Wrong taskCategoryId");
            }
            if ((model.Name == null || model.Name == String.Empty) || model.Name.Length > 200)
            {
                throw new ToDoListException("Wrong task name");
            }
         
        }

        public static void TaskCategoryModelValidation(TaskCategoryModel model)
        {
            if (model == null)
            {
                throw new ToDoListException("TaskCategory was null");
            }

            if ((model.Name == null || model.Name == String.Empty) || model.Name.Length > 100)
            {
                throw new ToDoListException("Wrong taskCategory name");
            }


        }

    }
}
