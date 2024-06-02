﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetProjeJWT.Controllers;

namespace NetProjeJWT.Token
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController(ITokenService tokenService) : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateClientToken(GetAccessTokenRequestDto request)
        {
            var response = await tokenService.CreateClientAccessToken(request);
            return CreateActionResult(response);
        }
    }
}
