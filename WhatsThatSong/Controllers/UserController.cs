using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhatsThatSong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BusinessLogicClass _businessLogicClass;
        private readonly ILogger<UserController> _logger;

        public UserController(BusinessLogicClass businessLogicClass, ILogger<UserController> logger)
        {
            _businessLogicClass = businessLogicClass;
            _logger = logger;
        }

        [HttpGet]
        [Route("create")]
        public async Task<User> CreateUser(string userName, string password, string email)
        {
            User newUser = await _businessLogicClass.CreatNewBC(userName, password, email);
            if(newUser != null)
            {
                return newUser;
            }
            else
            {
                return null;
            }
            
        }
        [HttpGet]
        [Route("login")]
        public async Task<User> loginUser(string userName, string password)
        {
            User LoggedInUser = await _businessLogicClass.LoginUser(userName, password);
            if(LoggedInUser != null)
            {
                return LoggedInUser;
            }
                return null;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _businessLogicClass.GetAllUsersAsync();
        }

        [HttpGet]
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _businessLogicClass.GetUserByIdAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _businessLogicClass.GetAllMessagesAsync();
        }


        /*
        public IActionResult Index()
        {
            return Ok();
        }
        */
    }
}
