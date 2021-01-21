using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger _logger;

        public DbSet<User> users;
        public DbSet<Song> songs;
        public DbSet<Message> messages;
        public DbSet<Artist> artists;
        public DbSet<Genre> genres;
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
            users.Update(UserInDb);
            await _applicationDbContext.SaveChangesAsync();
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
            await users.AddAsync(newUser);
            await _applicationDbContext.SaveChangesAsync();
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

        public async Task<List<User>> GetUsersByPartialN(string searchString)
        {
            List<User> list = new List<User>();
            List<User> usersToSearch = await users.ToListAsync();
            foreach(var x in usersToSearch)
            {
                if (x.UserName.Contains(searchString))
                {
                    list.Add(x);
                }
            }
            return list;
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
