﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TaskCategory: BaseEntity
    {

        [Required, StringLength(100)]
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set;}

    }
}
