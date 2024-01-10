using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TaskCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<int>? TaskIds { get; set; }
    }
}
