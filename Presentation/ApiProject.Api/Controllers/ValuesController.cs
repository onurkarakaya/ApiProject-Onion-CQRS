﻿using ApiProject.Application.Interfaces.UnitOfWorks;
using ApiProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
        private readonly IUnitOfWork _unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.GetReadRepository<Product>().GetAllAsync());
        }
    }
}
