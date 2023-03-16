using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OktaDomain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OktaDomain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
	private readonly ITokenService _tokenService;

	public AccountController(ITokenSerivice tokenService)
	{
		_tokenService = tokenService;
	}

        public async Task<IActionResult> SignInGetToken(string userName,string password)
        {
		var token= _tokenService.GetToken(userName,password);
        	if(token!=null)
		{
			return Ok(token);
		}
		else{
			return BadRequest();
		}
	}
    }
}
