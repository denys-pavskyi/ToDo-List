using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TaskCategoryId { get; set; }

        public Status Status { get; set; }
    }
}
