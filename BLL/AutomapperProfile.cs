using AutoMapper;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = DAL.Entities.Task;

namespace BLL
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Task, TaskModel>()
                .ReverseMap();

            CreateMap<TaskCategory, TaskCategoryModel>()
                .ForMember(tcm => tcm.TaskIds, tc => tc.MapFrom(x => x.Tasks.Select(x => x.Id)))
                .ReverseMap();


        }

    }
}
