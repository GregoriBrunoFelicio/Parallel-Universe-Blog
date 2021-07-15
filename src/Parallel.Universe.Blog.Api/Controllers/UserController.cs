﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.ViewModels;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] UserViewModel model)
        {
            await userRepository.UpdateAsync(mapper.Map<UserViewModel, User>(model));
            return await unitOfWork.CommitAsync() ? (IActionResult)Ok() : BadRequest();
        }


        [HttpPut("Inactivate/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Inactivate(int id)
        {
            var userFromDb = await userRepository.GetByIdAsync(id);

            if (userFromDb == null) return NotFound("User not found.");

            var user = new User(id, userFromDb.Name, userFromDb.About, userFromDb.Account, false);

            await userRepository.UpdateAsync(user);

            return await unitOfWork.CommitAsync() ? (IActionResult)Ok() : BadRequest();
        }
    }
}
