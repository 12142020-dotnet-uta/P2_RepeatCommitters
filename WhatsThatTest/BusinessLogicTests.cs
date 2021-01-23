using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BusinessLogicLayer;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using ModelLayer.Models;
using Microsoft.Extensions.Logging;
using ModelLayer.ViewModels;

namespace WhatsThatTest
{
    public class BusinessLogicTests
    {

        private readonly MapperClass _mapperClass;
        private readonly ILogger<Repository> _logger;


        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void GetAllUsersAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                _repository.users.Add(user);
                var listOfUsers = businessLogicClass.GetAllUsersAsync();
                Assert.NotNull(listOfUsers);
            }
        }

        [Fact]
        public void GetUserProfileViewModelAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                _repository.users.Add(user);
                Task<UserProfileViewModel> upvm = businessLogicClass.GetUserProfileViewModel(user.Id);
                Assert.NotNull(upvm);
            }
        }

        [Fact]
        public void CreateNewBCTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);

                Task<User> user = businessLogicClass.CreatNewBC("username","password","email");
                Assert.NotNull(user);
            }
        }

        [Fact]
        public void SaveUserToDbTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<User> usee = businessLogicClass.SaveUserToDb(user);
                Assert.NotNull(usee);
            }
        }

        [Fact]
        public void LoginUserTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<User> usee = businessLogicClass.SaveUserToDb(user);
                Task<User> loggedInUser = businessLogicClass.LoginUser(user.UserName,user.Password);
                Assert.NotNull(loggedInUser);
            }
        }

        [Fact]
        public void GetOriginalSongsByLyricsTest()
        {
            const string lyrics = "lorem ips subsciat boom bap da ting go skrrrrra ka ka pa pa pa";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var song = new Song
                {
                    Id = int.MaxValue,
                    ArtistId = int.MaxValue,
                    GenreId = int.MaxValue,
                    Title = "test song",
                    Duration = TimeSpan.MaxValue,
                    NumberOfPlays = int.MaxValue,
                    Lyrics = lyrics,
                    isOriginal = true
                };

                Task<List<Song>> listOfSongs = businessLogicClass.GetOriginalsongsByLyrics(lyrics);
                Assert.NotNull(listOfSongs);
            }
        }

        [Fact]
        public void RequestFriendTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);

                // Create a user to send request
                var user1 = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // Create a second user to receieve request
                var user2 = new User
                {
                    Id = int.MaxValue - 1,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@zmail.com"
                };

                Task friendRequest = businessLogicClass.RequesFriend(user1.Id, user2.Id);
                Assert.NotNull(friendRequest);
            }
        }

        [Fact]
        public void SearchForUsersByPartialN()
        {
            const string username = "LoremIpsSubsciat";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = username,
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<List<User>> listOfUsers = businessLogicClass.SearchForUsersByPartialN(username);
                Assert.NotNull(listOfUsers);
            }
        }

        [Fact]
        public void GetUserByIdAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
.Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<User> usee = businessLogicClass.SaveUserToDb(user);
                Task<User> loggedInUser = businessLogicClass.LoginUser(user.UserName, user.Password);
                Assert.NotNull(loggedInUser);
            }
        }

        [Fact]
        public void DeleteFriendTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
.Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<User> usee = businessLogicClass.SaveUserToDb(user);
                Task<User> loggedInUser = businessLogicClass.LoginUser(user.UserName, user.Password);
                Assert.NotNull(loggedInUser);
            }
        }

        [Fact]
        public void GetMessagesViewModelTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
.Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<User> usee = businessLogicClass.SaveUserToDb(user);
                Task<User> loggedInUser = businessLogicClass.LoginUser(user.UserName, user.Password);
                Assert.NotNull(loggedInUser);
            }
        }

        [Fact]
        public void SendMessageTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
.Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<User> usee = businessLogicClass.SaveUserToDb(user);
                Task<User> loggedInUser = businessLogicClass.LoginUser(user.UserName, user.Password);
                Assert.NotNull(loggedInUser);
            }
        }

        [Fact]
        public void GetAllMessageAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
.Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository _repository = new Repository(context, _logger);
                BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<User> usee = businessLogicClass.SaveUserToDb(user);
                Task<User> loggedInUser = businessLogicClass.LoginUser(user.UserName, user.Password);
                Assert.NotNull(loggedInUser);
            }
        }
    }
}
