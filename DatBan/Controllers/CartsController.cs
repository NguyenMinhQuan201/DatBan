using Domain.Features;
using Domain.Models.Dto.Order;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DatBan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public CartsController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("add-cart")]
        public async Task<IActionResult> Create([FromForm] OrderDetailDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderService.CreateDetail(request);
                if (result.IsSuccessed)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpPut("update-cart")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDetailDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderService.UpdateDetail(id, request);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }

            }
            return BadRequest();
        }
        [HttpDelete("delete-cart")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderService.DeleteDetail(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        //[Authorize(Policy = "Area_Index")]
        [HttpGet("get-by-name-cart")]
        public async Task<IActionResult> GetByname(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderService.GetAllOrderDetaill(pageSize, pageIndex, name);
                if (result.IsSuccessed)
                {
                    return Ok(result);
                }

            }

            return BadRequest();
        }
        [HttpGet("get-by-id-cart")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderService.GetByIdDetail(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        [HttpGet("get-full-cart")]
        public async Task<IActionResult> GetFull(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _orderService.GetAllOrderDetail(id);
                return Ok(result);
            }
        }
    }
}
