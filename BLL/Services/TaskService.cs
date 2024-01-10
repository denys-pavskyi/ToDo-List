using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Services
{
    public class TaskService: ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddTaskAsync(TaskModel model)
        {
            ModelsValidation.TaskModelValidation(model);
            var mappedTask = _mapper.Map<DAL.Entities.Task>(model);


            await _unitOfWork.TaskRepository.AddAsync(mappedTask);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTaskAsync(int modelId)
        {
            await _unitOfWork.TaskRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
        {
            IEnumerable<DAL.Entities.Task> unmappedTasks = await _unitOfWork.TaskRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<TaskModel>>(unmappedTasks);
        }

        public async Task<TaskModel> GetTaskByIdAsync(int id)
        {
            var unmappedTask = await _unitOfWork.TaskRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<TaskModel>(unmappedTask);
        }

        public async Task UpdateTaskAsync(TaskModel model)
        {
            ModelsValidation.TaskModelValidation(model);
            var mapped = _mapper.Map<DAL.Entities.Task>(model);
            _unitOfWork.TaskRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> UpdateTaskStatusByIdAsync(int taskId, int statusId)
        {
            if (!Enum.IsDefined(typeof(Status), statusId))
            {
                throw new ToDoListException("Wrong status id");
            }

            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            
            if(task == null)
            {
                return false;
            }

            task.Status = (Status)statusId;

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateTaskCategoryInTaskByIdAsync(int taskId, int categoryId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);

            if (task == null)
            {
                return false;
            }

            if((await _unitOfWork.TaskCategoryRepository.GetByIdAsync(categoryId)) == null)
            {
                throw new ToDoListException("Wrong category id");
            }

            task.TaskCategoryId = categoryId;
            await _unitOfWork.SaveAsync();
            return true;
        }
        
    }
}
