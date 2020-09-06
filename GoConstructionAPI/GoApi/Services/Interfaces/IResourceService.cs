﻿using GoApi.Data.Dtos;
using GoApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoApi.Services.Interfaces
{
    public interface IResourceService
    {
        Task CreateJobAsync(Site site, Job mappedJob, Guid oid, ApplicationUser user, bool IsRoot);
    }
}