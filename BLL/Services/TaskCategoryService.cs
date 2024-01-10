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
    public class TaskCategoryService: ITaskCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddTaskCategoryAsync(TaskCategoryModel model)
        {
            ModelsValidation.TaskCategoryModelValidation(model);
            var mappedTaskCategory = _mapper.Map<TaskCategory>(model);


            await _unitOfWork.TaskCategoryRepository.AddAsync(mappedTaskCategory);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTaskCategoryAsync(int modelId)
        {
            await _unitOfWork.TaskCategoryRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TaskCategoryModel>> GetAllTaskCategoriesAsync()
        {
            IEnumerable<TaskCategory> unmappedTaskCategories = await _unitOfWork.TaskCategoryRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<TaskCategoryModel>>(unmappedTaskCategories);
        }

        public async Task<TaskCategoryModel> GetTaskCategoryByIdAsync(int id)
        {
            var unmappedTaskCategory = await _unitOfWork.TaskCategoryRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<TaskCategoryModel>(unmappedTaskCategory);
        }

        public async Task UpdateTaskCategoryAsync(TaskCategoryModel model)
        {
            ModelsValidation.TaskCategoryModelValidation(model);
            var mapped = _mapper.Map<TaskCategory>(model);
            _unitOfWork.TaskCategoryRepository.Update(mapped);
            await _unitOfWork.SaveAsync();
        }
    }
}
