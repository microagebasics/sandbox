﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HaroldToDo.Models
{
    public class NewTodoItem
    {

        [Required]
        public string Title { get; set; }
    }
}