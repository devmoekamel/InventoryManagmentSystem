using InventoryClassLibrary.DTO;
using InventoryClassLibrary.DTO.Auth;
using InventoryClassLibrary.Enums;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Auth.Command.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO registerdata)
        {
            if(!ModelState.IsValid)
            {
                return Ok(ResponseDTO<object>.Error(
                    errorCode: ErrorCode.ValidationError));


            }
            ResultStatus  result =  await mediator.Send(new RegisterCommand { RegisterData = registerdata });

            if(!result.Status)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.UnexpectedError
                    , message: result.Message));
            }
            return Ok(ResponseDTO<object>.Success(data:result.Message));

        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginData)
        {


            if (!ModelState.IsValid)
            {
                return Ok(ResponseDTO<object>.Error(
                    errorCode: ErrorCode.ValidationError));


            }
            LoginResult result = await mediator.Send(new LoginCommand { LoginData = loginData });

            if (!result.Status)
            {
                return Ok(ResponseDTO<object>.Error(errorCode: ErrorCode.UnexpectedError
                    , message: result.Message));
            }
            return Ok(ResponseDTO<object>.Success(data: result.Message,expireDate:result.ExpireDate));
        }

    }
}
