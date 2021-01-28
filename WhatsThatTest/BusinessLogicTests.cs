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

        private readonly MapperClass _mapperClass = new MapperClass();
        private readonly ILogger<Repository> _logger;


        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task GetAllUsersAsyncTest()
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
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                await businessLogicClass.SaveNewUser(user);
                var listOfUsers = await businessLogicClass.GetAllUsersAsync();
                Assert.NotNull(listOfUsers);
            }
        }

        // Make Async all the way down through all layers
        // Some where there is something that is Asnyc and has not gotten the data yet.
        [Fact]
        public async Task GetUserProfileViewModelAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            await Task.Run(() =>
            {
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    Repository _repository = new Repository(context, _logger);
                    BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                    var user = new User
                    {
                        UserName = "jtest",
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123@email.com"
                    };

                    _repository.SaveNewUser(user).Wait();
                    var upvm = businessLogicClass.GetUserProfileViewModel(user.Id);
                    Assert.NotNull(upvm);
                }
            });
        }

        [Fact]
        public async Task CreateNewBCTest()
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

                User user = await businessLogicClass.CreatNewBC("username","password","email");
                Assert.NotNull(user);
            }
        }

        [Fact]
        public async Task SaveUserToDbTest()
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
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                await _repository.SaveNewUser(user);
                User u = await businessLogicClass.SaveUserToDb(user);
                Assert.NotNull(u);
            }
        }

        [Fact]
        public async Task LoginUserTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            await Task.Run(() =>
            {
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    Repository _repository = new Repository(context, _logger);
                    BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                    var user = new User
                    {
                        UserName = "jtest",
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123@email.com"
                    };

                    _repository.users.Add(user);
                    Task<User> loggedInUser = businessLogicClass.LoginUser(user.UserName, user.Password);
                    Assert.NotNull(loggedInUser);
                }
            });
        }

        [Fact]
        public async Task GetOriginalSongsByLyricsTest()
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
                    ArtistName = "Bad Posture",
                    Genre = "Pop Punk",
                    Title = "Yellow",
                    Duration = TimeSpan.MaxValue,
                    NumberOfPlays = int.MaxValue,
                    Lyrics = lyrics,
                    isOriginal = true
                };

                List<Song> listOfSongs = await businessLogicClass.GetOriginalsongsByLyrics(lyrics);
                Assert.NotNull(listOfSongs);
            }
        }

        [Fact]
        public async Task RequestFriendTest()
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
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // Create a second user to receieve request
                var user2 = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@zmail.com"
                };

                var friendRequest = await businessLogicClass.RequestFriend(user1.Id, user2.Id);
                Assert.NotNull(friendRequest);
            }
        }

        [Fact]
        public async Task SearchForUsersByPartialN()
        {
            const string username = "LoremIpsSubsciat";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            await Task.Run(() =>
            {
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    Repository _repository = new Repository(context, _logger);
                    BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                    var user = new User
                    {
                        UserName = username,
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123@email.com"
                    };

                    _repository.users.Add(user);
                    context.SaveChanges();
                    Task<List<User>> listOfUsers = businessLogicClass.SearchForUsersByPartialN(username);
                    Assert.NotNull(listOfUsers);
                }
            });
        }

        [Fact]
        public async Task GetUserByIdAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            await Task.Run(() =>
            {
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    Repository _repository = new Repository(context, _logger);
                    BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                    var user = new User
                    {
                        UserName = "jtest",
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123@email.com"
                    };

                    _repository.users.Add(user);
                    Task<User> userById = businessLogicClass.GetUserByIdAsync(user.Id);
                    Assert.NotNull(userById);
                }
            });
        }

        [Fact]
        public async Task DeleteFriendTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            await Task.Run(() =>
            {
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    Repository _repository = new Repository(context, _logger);
                    BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
                    // Create a user
                    var user1 = new User
                    {
                        UserName = "jtest",
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123@email.com"
                    };

                    // Create a second user
                    var user2 = new User
                    {
                        UserName = "jtest",
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123@zmail.com"
                    };

                    // instantiate friend list
                    FriendList fl = new FriendList(user1.Id, user2.Id);
                    _repository.friendList.Add(fl);
                    context.SaveChanges();

                    // make sure they're friends
                    Assert.Contains<FriendList>(fl, _repository.friendList);

                    // revoke friendship
                    businessLogicClass.DeleteFriend(user1.Id, user2.Id).Wait();
                    Assert.DoesNotContain<FriendList>(fl, _repository.friendList);

                }
            });
        }

        //[Fact]
        //public async void GetMessagesViewModelTest()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //    .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
        //    .Options;

        //    using (var context = new ApplicationDbContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repository _repository = new Repository(context, _logger);
        //        BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
        //        var user = new User
        //        {
        //            Id = int.MaxValue,
        //            UserName = "jtest",
        //            Password = "Test1!",
        //            FirstName = "Johnny",
        //            LastName = "Test",
        //            Email = "johnnytest123@email.com"
        //        };

        //        MessagingViewModel mvm = await businessLogicClass.GetMessagesViewModel(user.Id);
        //        Assert.NotNull(mvm);
        //    }
        //}

        //[Fact]
        //public void SendMessageTest()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //    .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
        //    .Options;

        //    using (var context = new ApplicationDbContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Database.EnsureCreated();

        //        Repository _repository = new Repository(context, _logger);
        //        BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);
        //        // Create a user
        //        var user1 = new User
        //        {
        //            Id = int.MaxValue,
        //            UserName = "jtest",
        //            Password = "Test1!",
        //            FirstName = "Johnny",
        //            LastName = "Test",
        //            Email = "johnnytest123@email.com"
        //        };

        //        // Create a second user
        //        var user2 = new User
        //        {
        //            Id = int.MaxValue - 1,
        //            UserName = "jtest",
        //            Password = "Test1!",
        //            FirstName = "Johnny",
        //            LastName = "Test",
        //            Email = "johnnytest123@zmail.com"
        //        };

        //        // It's about sending a message
        //        Task<MessagingViewModel> mvm = businessLogicClass.sendMessage(user2.Id,"content of message");
        //        Assert.NotNull(mvm);
        //    }
        //}

        [Fact]
        public async Task GetAllMessageAsyncTest()
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

                IEnumerable<Message> messages = await businessLogicClass.GetAllMessagesAsync();
                Assert.NotNull(messages);
            }
        }

        [Fact]
        public async Task GetLoggedInUserTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            await Task.Run(() =>
            {
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    Repository _repository = new Repository(context, _logger);
                    BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);

                    var user = new User
                    {
                        UserName = "jtest",
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123@email.com"
                    };

                    businessLogicClass.SaveNewUser(user).Wait();
                    Task<User> expectedUser = businessLogicClass.LoginUser(user.UserName, user.Password);

                    Assert.NotNull(expectedUser);
                }
            });
        }

        [Fact]
        public async Task GetSongByIdTest()
        {
            const string lyrics = "lorem ips subsciat boom bap da ting go skrrrrra ka ka pa pa pa";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            await Task.Run(() =>
            {
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    Repository _repository = new Repository(context, _logger);
                    BusinessLogicClass businessLogicClass = new BusinessLogicClass(_repository, _mapperClass, _logger);

                    var song = new Song
                    {
                        ArtistName = "Bad Posture",
                        Genre = "Pop Punk",
                        Title = "Yellow",
                        Duration = TimeSpan.MaxValue,
                        NumberOfPlays = int.MaxValue,
                        Lyrics = lyrics,
                        isOriginal = true
                    };

                    _repository.songs.Add(song);
                    context.SaveChanges();
                    Assert.NotNull(businessLogicClass.GetSongById(song.Id));
                }
            });
        }

        [Fact]
        public async Task AddSongToFavoritesTest()
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
                // create a user
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };
                // create a song
                var song = new Song
                {
                    ArtistName = "Bad Posture",
                    Genre = "Pop Punk",
                    Title = "Yellow",
                    Duration = TimeSpan.MaxValue,
                    NumberOfPlays = int.MaxValue,
                    Lyrics = lyrics,
                    isOriginal = true
                };

                // add the song to the user's favorite list
                await businessLogicClass.AddSongToFavorites(song.Id, user.Id);

                Assert.NotNull(_repository.favoriteLists);
            }
        }

        [Fact]
        public async Task PopulateDbTest()
        {

        }

        [Fact]
        public async Task GetNumOfFriendsByUserIdTest()
        {

        }
    }
}
