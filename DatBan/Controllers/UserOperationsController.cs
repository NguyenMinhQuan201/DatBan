using Domain.Features;
using Domain.Models.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace DatBan.Controllers
{
    public class UserOperationsController : Controller
    {
        private readonly IUserOperationService _UserOperationService;
        public UserOperationsController(IUserOperationService UserOperationService)
        {
            _UserOperationService = UserOperationService;
        }
        [HttpPost("add-UserOperation")]
        public async Task<IActionResult> Create([FromBody] UserOperationDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _UserOperationService.Create(request);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] UserOperationDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _UserOperationService.Update(id, request);
                if (result != null)
                {
                    return Ok(result);
                }

            }
            return BadRequest();
        }
        [HttpDelete("delete-UserOperation")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _UserOperationService.Delete(id);
                return Ok(result);
            }
        }
        [HttpGet("get-by-name-UserOperation")]
        public async Task<IActionResult> GetAll(int? pageSize, int? pageIndex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = await _UserOperationService.GetAll(pageSize, pageIndex, "");
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
                var result = await _UserOperationService.GetById(id);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
    }
}
