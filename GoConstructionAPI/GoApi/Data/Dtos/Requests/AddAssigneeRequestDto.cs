﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoApi.Data.Dtos
{
    public class AddAssigneeRequestDto
    {
        [Required]
        public string UserId { get; set; }
    }
}
