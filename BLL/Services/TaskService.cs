using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task AddAsync(TaskModel model)
        {
            ModelsValidation.TaskModelValidation(model);
            var mappedTask = _mapper.Map<DAL.Entities.Task>(model);


            await _unitOfWork.TaskRepository.AddAsync(mappedTask);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await _unitOfWork.TaskRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            IEnumerable<DAL.Entities.Task> unmappedTasks = await _unitOfWork.TaskRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<TaskModel>>(unmappedTasks);
        }

        public async Task<TaskModel> GetByIdAsync(int id)
        {
            var unmappedTask = await _unitOfWork.TaskRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<TaskModel>(unmappedTask);
        }

        public async Task UpdateAsync(TaskModel model)
        {
            ModelsValidation.TaskModelValidation(model);
            var mapped = _mapper.Map<DAL.Entities.Task>(model);
            _unitOfWork.TaskRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }
    }
}
