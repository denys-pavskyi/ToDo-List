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
    }
}
