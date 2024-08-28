using ApiProject.Application.Features.Brands.Commands.CreateBrand;
using ApiProject.Application.Features.Brands.Queries.GetAllBrands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrands(CreateBrandCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(brands);
        }

    }
}
