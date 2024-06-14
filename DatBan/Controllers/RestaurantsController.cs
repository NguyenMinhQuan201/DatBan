using Domain.Features;
using Domain.Models.Dto.ImportInvoiceDto;
using Domain.Models.Dto.Product;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _RestaurantService;
        public RestaurantsController(IRestaurantService RestaurantService)
        {
            _RestaurantService = RestaurantService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> Create([FromForm] RestaurantDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _RestaurantService.Create(request);
                if (result.IsSuccessed)
                {
                    return Ok(result.IsSuccessed);
                }
            }
            return BadRequest();
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] RestaurantDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _RestaurantService.Update(id, request);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }
            return BadRequest();
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _RestaurantService.Delete(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        //[Authorize(Policy = "Restaurant_Index")]
        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetByname(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _RestaurantService.GetByName(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result);
                }

            }

            return BadRequest();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _RestaurantService.GetById(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        [HttpGet("get-full")]
        public async Task<IActionResult> GetFull()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _RestaurantService.GetAll();
                return Ok(result);
            }
        }
    }
}
