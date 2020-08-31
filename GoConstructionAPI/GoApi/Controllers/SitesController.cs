﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GoApi.Data;
using GoApi.Data.Constants;
using GoApi.Data.Dtos;
using GoApi.Data.Models;
using GoApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SitesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBackgroundTaskQueue _queue;


        public SitesController(
            UserManager<ApplicationUser> userManager,
            AppDbContext appDbContext,
            IMapper mapper,
            IAuthService authService,
            IServiceScopeFactory serviceScopeFactory,
            IBackgroundTaskQueue queue
            )
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _authService = authService;
            _serviceScopeFactory = serviceScopeFactory;
            _queue = queue;
        }

        [HttpPost]
        [Authorize(Policy = Seniority.ContractorOrAbovePolicy)]
        public async Task<IActionResult> PostSites([FromBody] SiteCreateRequestDto model)
        {

            var mappedSite = _mapper.Map<Site>(model);
            var oid = _authService.GetRequestOid(Request);
            var user = await _userManager.GetUserAsync(User);
            mappedSite.Oid = oid;
            mappedSite.CreatedByUserId = user.Id;
            mappedSite.CreatedAt = DateTime.UtcNow;
            mappedSite.IsActive = true;

            await _appDbContext.AddAsync(mappedSite);
            await _appDbContext.SaveChangesAsync();

            return Ok();

        }

        [HttpGet]
        [Authorize(Policy = Seniority.WorkerOrAbovePolicy)]
        public IActionResult GetSites()
        {
            var oid = _authService.GetRequestOid(Request);
            var sites = _appDbContext.Sites.Where(s => s.Oid == oid && s.IsActive == true);
            var mappedSites = _mapper.Map<IEnumerable<SiteReadResponseDto>>(sites);
            return Ok(mappedSites);

        }




        //[HttpGet]
        //[Route("{siteId}")]
        //[Authorize(Policy = Seniority.WorkerOrAbovePolicy)]
        //public async Task<IActionResult> GetSiteDetail(Guid siteId)
        //{
        //    var site = await _appDbContext.Sites.FirstOrDefaultAsync(s => s.Id == siteId);
        //    var oid = _authService.GetRequestOid(Request);


        //}


    }
}
