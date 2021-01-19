using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Returns all messages for a user.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await messages.ToListAsync();
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
