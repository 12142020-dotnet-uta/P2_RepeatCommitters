using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.ViewModels;

namespace BusinessLogicLayer
{
    public class BusinessLogicClass
    {
        private readonly Repository _repository;
        private readonly MapperClass _mapperClass;
        private readonly ILogger _logger;

        public BusinessLogicClass(Repository repository, MapperClass mapperClass, ILogger<Repository> logger)
        {
            _repository = repository;
            _mapperClass = mapperClass;
            _logger = logger;
        }
        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

        public async Task<UserProfileViewModel> GetUserProfileViewModel(int id)
        {
            UserProfileViewModel model = await _mapperClass.BuildUserProfileViewModel(id);
            return model;
        }

        /// <summary>
        /// checks to see if a user with that info already exists and returns null if the user already exist. creates a new user if the ures does not already exist.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> CreatNewBC(string userName, string password, string email)
        {
            bool userExists = await _repository.DoesUserExist(userName, password);
            if(!userExists)
            {
                return null;
            }
            else 
            {
                User newUser = await _repository.CreateNewUser(userName, password,email);
                return newUser;
            }
        }

        public async Task<User> SaveUserToDb(User userToEdit)
        {
           await _repository.SaveUserToDb(userToEdit);
            return null;
        }

        /// <summary>
        /// checks to see if the user exists and logs them in if they do. returns null if they dont exist
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> LoginUser(string userName, string password)
        {
            bool userExists = await _repository.DoesUserExist(userName, password);
<<<<<<< HEAD
            if(userExists  == true)
=======
            if(userExists)
>>>>>>> 1bc886efcb3a6e0662dedd4f31747778994b1122
            {
                return await _repository.GetUserByNameAndPass(userName,password);
            }
            else
            {
                return null;
            }
        }

        public async Task<User> RequesFriend(int userid, int RerequestedFriendId)
        {
            _repository.RequestFreind(userid, RerequestedFriendId);
            return null;
        }

        public async Task<List<User>> SearchForUsersByPartialN(string searchstring)
        {
            List<User> ListOfUsers = await _repository.GetUsersByPartialN(searchstring);
            return ListOfUsers;
        }

        /// <summary>
        /// Returns a User specified by their id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetUserByIdAsync(id);
        }

        /// <summary>
        /// Returns all messages for a user.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _repository.GetAllMessagesAsync();
        }
    }
}
