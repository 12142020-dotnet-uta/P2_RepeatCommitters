﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Repository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        readonly ILogger _logger;

        public DbSet<User> users;
        public DbSet<Song> songs;
        public DbSet<Message> messages;
        public DbSet<Artist> artists;
        public DbSet<Genre> genres;
        public DbSet<FriendList> friendList;
        //public DbSet<>

        public Repository(ApplicationDbContext applicationDbContext, ILogger<Repository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            this.users = _applicationDbContext.Users;
            this.songs = _applicationDbContext.Songs;
            this.messages = _applicationDbContext.Messages;
            this.artists = _applicationDbContext.Artists;
            this.genres = _applicationDbContext.Genres;
        }

        public async Task<int> GetNumOfFriendsByUserId(int id)
        {   

            foreach(var item in friendList)
            {
                if(item.FriendId == id || item.RequestedFriendId == id)
                {

                }
            }
            return await friendList.Count(x => x.FriendId == id || x.RequestedFriendId == id);
        }

        public async Task<bool> DoesUserExist(string username, string passw)
        {
            User user = await users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == passw);
            if (user!=null){
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<User> GetUserByNameAndPass(string username, string passw)
        {
            return await users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == passw);
        }

        public async Task<User> SaveUserToDb(User userToEdit)
        {
            User UserInDb = userToEdit; 
             _applicationDbContext.SaveChanges();
            return null;
        }

        /// <summary>
        /// creates a new user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> CreateNewUser(string userName, string password, string email)
        {
            User newUser = new User(userName, password, email);
            _applicationDbContext.SaveChanges();
            return await users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
        }
        

        /// <summary>
        /// Returns all messages for a user.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await messages.ToListAsync();
        }

        public async void RequestFreind(int userId, int RerequestedFriendId)
        {
            FriendList request = new FriendList(userId, RerequestedFriendId);
            friendList.Add(request);
            _applicationDbContext.SaveChanges();
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUsersByPartialN(string searchString)
        {
            List<User> list = new List<User>();
            foreach(var x in users)
            {
                if (x.UserName.Contains(searchString))
                {
                    list.Add(x);
                }
            }
            return list ;
        }

        /// <summary>
        /// Returns all users to a list.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await users.ToListAsync();
        }

        /// <summary>
        /// Returns a User specified by their id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await users.FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
