using Domain.Features;
using Domain.Models.Dto.Category;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _Categoryservice;
        public CategoriesController(ICategoryService Categoryservice)
        {
            _Categoryservice = Categoryservice;
        }
        [HttpPost("add")]
        public async Task<IActionResult> Create([FromForm] CategoryDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _Categoryservice.Create(request);
                if (result.IsSuccessed)
                {
                    return Ok(result.IsSuccessed);
                }
            }
            return BadRequest();
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _Categoryservice.Update(id, request);
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
                var result = await _Categoryservice.Delete(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        //[Authorize(Policy = "Category_Index")]
        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetByname(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _Categoryservice.GetAll(pageSize, pageIndex, name);
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
                var result = await _Categoryservice.GetById(id);
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
                var result = await _Categoryservice.GetAll();
                return Ok(result);
            }
        }
    }
}
