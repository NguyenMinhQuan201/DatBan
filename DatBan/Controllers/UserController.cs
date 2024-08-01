using Domain.Features;
using Domain.Models.Dto.LoginDto;
using Domain.Models.Dto.RoleDto;
using Domain.Models.Dto.UserDto;
using EmailApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private const string INDEX_CST = "User_Index";
        private const string CREATE_CST = "User_Create";
        private const string EDIT_CST = "User_Edit";
        private const string DELETE_CST = "User_Delete";
        private const string ADMIN_CST = "admin";
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;

        public UserController(IUserService userService, IEmailSender emailSender)
        {
            _userService = userService;
            _emailSender = emailSender;
        }
        [HttpPost("renewToken")]
        public async Task<IActionResult> RenewToken(TokenRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var resultToken = await _userService.RenewToken(request);
                if (resultToken.IsSuccessed)
                {
                    return Ok(new LoginResponDto
                    {
                        Time = 3,
                        Token = resultToken.ResultObj.Access_Token,
                        RefreshToken = resultToken.ResultObj.Refresh_Token,
                        Status = true
                    });
                }
            }
            return BadRequest();
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResponDto
                {
                    Status = false
                });
            }
            else
            {
                var resultToken = await _userService.Login(request);

                if (resultToken.IsSuccessed==false)
                {
                    return Ok(new LoginResponDto
                    {
                        Status = false
                    });
                }

                return Ok(new LoginResponDto
                {
                    Time = 3,
                    Token = resultToken.ResultObj.Access_Token,
                    RefreshToken = resultToken.ResultObj.Refresh_Token,
                    Status = true
                });
            }


        }
        [HttpGet("forgot")]
        [AllowAnonymous]
        public async Task<IActionResult> SendMailCheckUser(string email, string uri)
        {
            var resultToken = await _userService.SendMailCheckUser(email, uri);
            if (!resultToken.IsSuccessed)
            {
                return BadRequest("Có lỗi xảy ra");
            }
            var message = new Message(new string[] { email }, "ForgotPassword", resultToken.ResultObj.ClientUrl);

            _emailSender.SendEmailAsync(message);
            return Ok(resultToken);
        }
        [HttpPost("renew-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ReNewPassword([FromBody] RenewPassword obj)
        {
            var resultToken = await _userService.RenewPassword(obj);
            if (!resultToken.IsSuccessed)
            {
                return BadRequest("Có lỗi xảy ra");
            }
            return Ok(resultToken.IsSuccessed);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Create(request);
            if (result == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Update(id, request);
            if (result == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Delete(id);
            if (result == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging(int? pageSize, int? pageIndex, string? name)
        {
            var products = await _userService.GetAll(pageSize,  pageIndex, name);
            return Ok(products);
        }
        //[Authorize(Policy = INDEX_CST)]
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var products = await _userService.GetById(Id);
            return Ok(products);
        }
        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RoleAssign(id, request);
            if (result == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        //Login GG
        [HttpGet("LoginGG")]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "User");
            var properties = _userService.GetProperties(redirectUrl);
            return new ChallengeResult("Google", properties);
        }
        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await _userService.GetTokenByLoginGG();
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
