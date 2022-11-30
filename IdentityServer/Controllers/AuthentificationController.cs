using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTOs;
using User.Application.Services;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AuthentificationController(IAccountService accountservice, IMapper mapper)
        {
            _accountService = accountservice;
            _mapper = mapper;   
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(NewAccountModel model)
        {
            //var res = _mapper.Map<User.Application.Entities.User>(model);
            await _accountService.CreateAccount(model);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AuthentificatedAccount(string userName, string password)
        {
            var model = new AuthenticationAccountDto()
            {
                UserName = userName,
                Password = password,
            };

            var res = await _accountService.AuthenticationAccount(model);
            return Ok(res);
        }
    }
}
