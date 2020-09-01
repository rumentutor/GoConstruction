﻿using GoApi.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoApi.Data.Dtos
{
    public class ResetPasswordRequestDto
    {
        [Required]
        [ValidEmail(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
    }
}
