using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using Application.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("usersAtEstablishment")]
        [Authorize(Roles = "establishment_admin")]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUsersAtEstablishment()
        {
            return HandleResult(await _userService.GetUsersAtEstablishment());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "establishment_admin, system_admin")]
        public async Task<ActionResult<AppUserDto>> GetUser(string id)
        {
            return Ok(await _userService.GetUser(id));
        }

        [HttpPost]
        [Authorize(Roles = "establishment_admin, system_admin")]
        public async Task<ActionResult<AppUserDto>> CreateUser(RegisterDto user)
        {
            return HandleResult(await _userService.CreateUser(user));
        }
    }
}