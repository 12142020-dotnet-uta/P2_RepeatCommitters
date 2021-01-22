using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using ModelLayer.ViewModels;
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
        public async Task<UserProfileViewModel> CreateUser(string userName, string password, string email)
        {
            //User user = await _businessLogicClass.CreatNewBC("ronald", "mcdonald", "ronald@mcdonald.com");
            User newUser = await _businessLogicClass.CreatNewBC(userName, password, email);
            UserProfileViewModel UPVM = await _businessLogicClass.GetUserProfileViewModel(newUser.Id);
            if(newUser != null)
            {
                return UPVM;
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
        /// <summary>
        /// Gets the user to edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("EditButton")]
        public async Task<User> GetUserToEdit(int id)
        {
            User user = await _businessLogicClass.GetUserByIdAsync(id);
            return user;
        }

        [HttpPost]
        [Route("SaveEdit")]
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
        [Route("RequestFriend")]
        public async Task<User> FriendRequest(int userId, int requestedFriendId)
        {
            await _businessLogicClass.RequesFriend(userId, requestedFriendId);
            User LoggedInUser = await _businessLogicClass.GetUserByIdAsync(userId);
            return LoggedInUser;
        }

        /// <summary>
        /// sends a list of friends that have been accepted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RequestFriend")]
        public async Task<List<FriendList>> GetFriendsByUserId(int id)
        {
           List<FriendList> friendList = await _businessLogicClass.GetListOfFriendsByUserId(id);
            return  friendList;
        }

        /// <summary>
        /// RETURNS A MESSAGEVIEWMODEL WITH ALL OF THE MESSAGES BETWEEN 2 USERS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GoToChat")]
        public async Task<MessagingViewModel> GetMessagesBetween2Users(int UserToMessageId)
        {
            MessagingViewModel viewModel = await _businessLogicClass.GetMessagesViewModel(UserToMessageId);
            return viewModel;
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
