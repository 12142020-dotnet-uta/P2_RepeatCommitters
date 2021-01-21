using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
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
        [Route("editUser")]
        public async Task<User> EditUser(int userId, string userName, string password, string email, string firstName, string lastName)
        {
            User userToEdit = await _businessLogicClass.GetUserByIdAsync(userId);
            userToEdit.UserName = userName; userToEdit.Password = password; userToEdit.Email = email; userToEdit.FirstName = firstName; userToEdit.LastName = lastName;
            await _businessLogicClass.SaveUserToDb(userToEdit);
            return userToEdit; 
        }
        [HttpGet]
        [Route("SearchForUsers")]
        public async Task<List<User>> SearchForUsers(string searchString)
        {
            List<User> listOfUsers = await _businessLogicClass.SearchForUsersByPartialN(searchString);
            
            return listOfUsers;
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
    }
}
