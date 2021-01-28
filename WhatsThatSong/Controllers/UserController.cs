using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsThatSong.Controllers
{
    
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly BusinessLogicClass _businessLogicClass;
        private readonly ILogger<UserController> _logger;


        public UserController(BusinessLogicClass businessLogicClass, ILogger<UserController> logger)
        {
            _businessLogicClass = businessLogicClass;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<UserProfileViewModel> CreateUser(string userName, string password, string email)
        {
            User newUser = await _businessLogicClass.CreatNewBC(userName, password, email);
            UserProfileViewModel UPVM = await _businessLogicClass.GetUserProfileViewModel(newUser.Id);
            if(newUser.UserName==null)
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
        public async Task<UserProfileViewModel> login(string userName, string password)
        {
            User LoggedInUser = await _businessLogicClass.LoginUser(userName, password);
            UserProfileViewModel UPVM = await _businessLogicClass.GetUserProfileViewModel(LoggedInUser.Id);

            if (LoggedInUser != null)
            {
                return UPVM;
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

        [HttpPut]
        [Route("SaveEdit")]
        public async Task<UserProfileViewModel> EditUser(int userId, string userName, string password, string email, string firstName, string lastName)
        {
            User userToEdit = await _businessLogicClass.GetUserByIdAsync(userId);
            userToEdit.UserName = userName; userToEdit.Password = password; userToEdit.Email = email; userToEdit.FirstName = firstName; userToEdit.LastName = lastName;
            await _businessLogicClass.SaveUserToDb(userToEdit);
            UserProfileViewModel UPVM = await _businessLogicClass.GetUserProfileViewModel(userToEdit.Id);
            return UPVM;
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
        public async Task FriendRequest(int userId, int requestedFriendId)
        {
            await _businessLogicClass.RequestFriend(userId, requestedFriendId);
           // User LoggedInUser = await _businessLogicClass.GetUserByIdAsync(userId);
        }


        [HttpPut]
        [Route("AcceptFriend")]
        public async Task AcceptFriend(int UserId, int pendingFriendId)
        {
            await _businessLogicClass.AcceptFriend(UserId,pendingFriendId);
        }
        /// <summary>
        /// sends a list of friends that have been accepted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFriends")]
        public async Task<List<FriendList>> GetFriendsByUserId(int id)
        {
           List<FriendList> friendList = await _businessLogicClass.GetListOfFriendsByUserId(id);
            return  friendList;
        }
        /// <summary>
        /// deletes a friend from the friend list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteFriend")]
        public async Task<List<FriendList>> DeleteFriend(int UserId, int friendToDeleteId)
        {
            await _businessLogicClass.DeleteFriend(UserId, friendToDeleteId);
            List<FriendList> friendList = await _businessLogicClass.GetListOfFriendsByUserId(UserId);
            return friendList;
        }

        [HttpPost]
        [Route("sendMessage")]
        public async Task<MessagingViewModel> SendMessage(string FromUserName, int LoggedInUserIdint,int UserToMessageId, string content)
        {
            MessagingViewModel viewModel = await _businessLogicClass.sendMessage(FromUserName, LoggedInUserIdint, UserToMessageId, content);
            //MessagingViewModel viewModel = await _businessLogicClass.GetMessagesViewModel(UserToMessageId);
            return viewModel;
        }

        /// <summary>
        /// RETURNS A MESSAGEVIEWMODEL WITH ALL OF THE MESSAGES BETWEEN 2 USERS based in both user id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GoToChat")]
        public async Task<MessagingViewModel> GetMessagesBetween2Users(int loggedInUser, int UserToMessageId)
        {
            MessagingViewModel viewModel = await _businessLogicClass.GetMessagesViewModel(loggedInUser, UserToMessageId);
            return viewModel;
        }

        

        [HttpGet]
        [Route("BakToProfile")]
        public async Task<UserProfileViewModel> BackToProfile(int LoggedInUserid)
        {
            User user = await _businessLogicClass.GetUserByIdAsync(LoggedInUserid);
            UserProfileViewModel viewModel = await _businessLogicClass.GetUserProfileViewModel(LoggedInUserid);
            return viewModel;
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _businessLogicClass.GetAllUsersAsync();
        }

        [HttpGet]
        [Route("getUserByIdaAync")]
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _businessLogicClass.GetUserByIdAsync(id);
        }

        [HttpGet]
        [Route("GetAllMessagesAsync")]
        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _businessLogicClass.GetAllMessagesAsync();
        }
    }
}
