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
        private ApplicationDbContext _applicationDbContext;
        readonly ILogger _logger;

        public DbSet<User> users;
        public DbSet<Song> songs;
        public DbSet<Message> messages;
        public DbSet<Artist> artists;
        public DbSet<Genre> genres;
        public DbSet<FriendList> friendList;
        public DbSet<FavoriteList> favoriteLists;
       
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
            this.friendList = _applicationDbContext.FriendList;
            this.favoriteLists = _applicationDbContext.FavoriteLists;
            populateDb();
        }


        /// <summary>
        /// adds a song to the favorites of a user if it isnt already a favorite of that user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task AddSongToFavorites(int songid, int loggedInUserId)
        {
            FavoriteList SongToAdd = new FavoriteList();
            foreach(var item in favoriteLists)
            {
                if(item.SongId == songid && item.UserId == loggedInUserId)
                {
                    SongToAdd = item; 
                }
            }
            if(SongToAdd == null)
            {
                SongToAdd.UserId = loggedInUserId;
                SongToAdd.SongId = songid;
                favoriteLists.Add(SongToAdd);
                _applicationDbContext.SaveChanges();
            }
        }

        public async Task<List<FavoriteList>> GetUsersFavorites(int userId)
        {
            return await favoriteLists.Where(item => item.UserId == userId).ToListAsync();
        }

        public async Task<Song> GetSongById(int id)
        {
            return await songs.FirstOrDefaultAsync(item => item.Id == id);
        }

        public void populateDb()
        {
            if (users == null)
            {
                User user = new User("Ronald", "Mcdonald", "ronald@Mcdonald.com");
                users.Add(user);
                _applicationDbContext.SaveChanges();
            }
        }

        public async Task<string> HasPendingFrinedRequest(int id)
        {
            string hasPendingRequest ="";
            await foreach(var item in friendList)
            {
                if((item.FriendId == id || item.RequestedFriendId == id) && item.status == "Pending")
                {
                    hasPendingRequest = "Pending";
                }
            }
            return hasPendingRequest;
        }

        public async Task<List<Song>> GetOriginalSongsByGenre(string genre)
        {
            List<Song> OriginalSongs = new List<Song>();
           await foreach(var item in songs)
            {
                if(item.isOriginal == true)
                {
                    OriginalSongs.Add(item);
                }
            }
            return OriginalSongs;
        }

        public async Task<int> GetNumOfFriendsByUserId(int id)
        {
            int numOfFriends = 0;
            await foreach(var item in friendList)
            {
                if((item.FriendId == id || item.RequestedFriendId == id)&& (item.status == "accept"))
                {
                    numOfFriends += 1;
                }
            }
            return numOfFriends;
        }

        /// <summary>
        /// returns the top 5 songs based on the number of plays
        /// </summary>
        /// <returns></returns>
        public async Task<List<Song>> GetTop5Originals()
        {
            int count = 0;
            List<Song> allSongs = await getAllSongs();
            List<Song> fiveSongs = new List<Song>();

            do
            {
                Song highest = new Song();
                foreach (var item in allSongs)
                {
                    if(item.NumberOfPlays > highest.NumberOfPlays)
                    {
                        highest = item;
                    }
                }
                fiveSongs.Add(highest);
                allSongs.Remove(highest);
                count++;
            }
            while (fiveSongs.Count < 5);
            {

            }
            return fiveSongs; 
        }

        public async Task IncrementNumPlays(int songId)
        {
            Song songToIncrement = await GetSongById(songId);
            songToIncrement.NumberOfPlays += 1;
            _applicationDbContext.SaveChanges();
        }

        public async Task<List<Song>> getAllSongs()
        {
            List<Song> x = new List<Song>();
            await foreach(var item in songs)
            {
                x.Add(item);
            }
            return x;
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

        public async Task<List<Song>> GetOriginalSongByLyrics(string phrase)
        {
            List<Song> songlist = new List<Song>();
            await foreach (var item in songs)
            {
                if(item.Lyrics.Contains(phrase))
                {
                    songlist.Add(item);
                }
            }
            return songlist;
        }

        public async Task<User> GetUserByNameAndPass(string username, string passw)
        {
            return await users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == passw);
        }

        public async Task<User> SaveUserToDb(User userToEdit)
        {
            User UserInDb = userToEdit;

            _applicationDbContext.SaveChanges();
            return UserInDb;

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

        public async Task<List<FriendList>> GetListOfFriendsByUserId(int id)
        {
            //List<FriendList> list = new List<FriendList>();
            //foreach(var item in friendList)
            //{
            //    if((item.FriendId == id || item.RequestedFriendId == id))
            //    {
            //        list.Add(item);
            //    }
            //}
            return await friendList.Where(item => item.FriendId == id || item.RequestedFriendId == id && item.status == "accept").ToListAsync();
        }

        public async Task SaveNewUser(User user)
        {
             users.Add(user);
             _applicationDbContext.SaveChanges();
        }

        public async Task DeletFriend(int id1, int id2)
        {
            List<FriendList> listToDelete = new List<FriendList>();
            foreach(var item in friendList)
            {
                if(item.RequestedFriendId == id1 && item.FriendId == id2)
                {
                    friendList.Remove(item);
                    _applicationDbContext.SaveChanges();
                }
                else if (item.RequestedFriendId == id2 && item.FriendId == id1)
                {
                    friendList.Remove(item);
                    _applicationDbContext.SaveChanges();
                }
            }
            
            //List<FriendList> listToDelete = friendList.Where(item => item.RequestedFriendId == id1 && item.FriendId == id2 ||
            //item.RequestedFriendId == id2 && item.FriendId == id1);
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
        }

        public async Task SaveMessage(int userToMessageId, int loggedInId, string content)
        {

            Message message = new Message(userToMessageId, loggedInId, content);
            messages.Add(message);
            _applicationDbContext.SaveChanges();
        }

        public async Task<List<Message>> GetMessages2users(int id, int userToMessage)
        {
            return await messages.Where(item => item.FromUserId == id && item.ToUserId == userToMessage ||
            item.ToUserId == id && item.FromUserId == userToMessage).ToListAsync();
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
        public async Task<List<User>> GetAllUsersAsync()
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
