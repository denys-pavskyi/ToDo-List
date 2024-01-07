using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Task: BaseEntity
    {

        [Required, StringLength(200)]
        public string Name { get; set; }

        
        public int TaskCategoryId { get; set; }

        public TaskCategory TaskCategory { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        Backlog, 
        InProgress, 
        Done, 
        Complete
    }
}
