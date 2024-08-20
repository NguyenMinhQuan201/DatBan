using Domain.Features;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        //thay đổi sang cho tes2
        private readonly IAreaService _Areaservice;
        public AreasController(IAreaService Areaservice)
        {
            _Areaservice = Areaservice;
        }
        [HttpPost("add")]
        public async Task<IActionResult> Create([FromForm] AreaDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _Areaservice.Create(request);
                if (result.IsSuccessed)
                {
                    return Ok(result.IsSuccessed);
                }
            }
            return BadRequest();
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] AreaDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _Areaservice.Update(id, request);
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
                var result = await _Areaservice.Delete(id);
                if (result.IsSuccessed)
                {
                    return Ok(result.ResultObj);
                }
            }
            return BadRequest();
        }
        //[Authorize(Policy = "Area_Index")]
        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetByname(int? pageSize, int? pageIndex, string? name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _Areaservice.GetAll(pageSize, pageIndex, name);
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
                var result = await _Areaservice.GetById(id);
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
                var result = await _Areaservice.GetAll();
                return Ok(result);
            }
        }
    }
}
