﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoApi.Data.Dtos
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
