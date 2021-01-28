using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WhatsThatTest
{
    public class RepositoryTests
    {
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

                Repository repository = new Repository(context, _logger);
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                await repository.SaveNewUser(user);
                var listOfUsers = await repository.GetAllUsersAsync();
                Assert.NotNull(listOfUsers);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task GetUserByIdAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _logger);
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                await repository.SaveNewUser(user);
                var userById = await repository.GetUserByIdAsync(user.Id);
                Assert.NotNull(userById);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task DoesUserExistTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _logger);
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                await repository.SaveNewUser(user);
                var userExists = await repository.DoesUserExist("jtest", "Test1!");
                Assert.True(userExists);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task GetUserByNameAndPassTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _logger);
                var user = new User
                {
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                await repository.SaveNewUser(user);
                var userByNameAndPass = await repository.GetUserByNameAndPass("jtest", "Test1!");
                Assert.NotNull(userByNameAndPass);
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

                Repository repository = new Repository(context, _logger);

                // initial user
                var user = new User
                {
                    Id=int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                await repository.SaveNewUser(user);
                await repository.GetUserByIdAsync(user.Id);

                // edited user
                var editedUser = new User
                {
                    Id=int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Assert.NotNull(await repository.SaveUserToDb(editedUser));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task CreateNewUserTest()
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

                    Repository repository = new Repository(context, _logger);
                    var user = new User
                    {
                        UserName = "jtest",
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123@email.com"
                    };

                    Assert.NotNull(repository.CreateNewUser(user.UserName, user.Password, user.Email));
                }
            });
        }

        [Fact]
        public async Task AddSongToFavoritesTest()
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

                    Repository repository = new Repository(context, _logger);
                    // Create a new user
                    var user = new User
                    {
                        UserName = "jtest",
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123@email.com"
                    };
                    // Create a song to favorite
                    var song = new Song
                    {
                        ArtistName = "Bad Posture",
                        Genre = "Pop Punk",
                        Title = "Yellow",
                        Duration = TimeSpan.MaxValue,
                        NumberOfPlays = int.MaxValue,
                        Lyrics = "Lorem ips subsciat",
                        isOriginal = true
                    };

                    repository.SaveNewUser(user).Wait();
                    repository.SaveSongToDb(song).Wait();

                    // attempt to add song to favorite list
                    repository.AddSongToFavorites(song.Id, user.Id).Wait();
                    // get list of users favorite songs
                    Assert.NotNull(repository.GetUsersFavorites(user.Id));
                }
            });
        }

        //[Fact]
        //public async Task GetUsersFavoritesTest() { }

        [Fact]
        public async Task GetSongByIdTest()
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

                    Repository repository = new Repository(context, _logger);
                    // Create a song
                    var song = new Song
                    {
                        ArtistName = "Bad Posture",
                        Genre = "Pop Punk",
                        Title = "Yellow",
                        Duration = TimeSpan.MaxValue,
                        NumberOfPlays = int.MaxValue,
                        Lyrics = "Lorem ips subsciat",
                        isOriginal = true
                    };

                    repository.SaveSongToDb(song).Wait();
                    Task<Song> s = repository.GetSongById(song.Id);

                    Assert.NotNull(s);
                }
            });
        }

        [Fact]
        public void GetMessagesToUsersTest() { }

        [Fact]
        public void GetSongsBySearchGenreTest() { }

        [Fact]
        public async Task GetTop5OriginalsTest()
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

                    Repository repository = new Repository(context, _logger);
                    // create 5 songs
                    for (int i = 0; i < 5; i++)
                    {
                        var song = new Song
                        {
                            Id = int.MaxValue-i,
                            ArtistName = "Bad Posture",
                            Genre = "Pop Punk",
                            Title = "Yellow",
                            Duration = TimeSpan.MaxValue,
                            NumberOfPlays = int.MaxValue,
                            Lyrics = "Lorem ips subsciat",
                            isOriginal = true
                        };

                        repository.SaveSongToDb(song).Wait();
                    }

                    Task<List<Song>> listOf5Songs = repository.GetTop5Originals();
                    Assert.Equal(5, listOf5Songs.Result.Count);
                }
            });
        }

        /*[Fact]
        public async Task IncrementNumPlaysTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _logger);
                // Create a song with 1 less than max value number of plays
                var song = new Song
                {
                    ArtistName = "Bad Posture",
                    Genre = "Pop Punk",
                    Title = "Yellow",
                    Duration = TimeSpan.MaxValue,
                    NumberOfPlays = int.MaxValue - 1,
                    Lyrics = "Lorem ips subsciat",
                    isOriginal = true
                };

                repository.SaveSongToDb(song).Wait();
                repository.IncrementNumPlays(song.Id).Wait();
                Assert.Equal(int.MaxValue, song.NumberOfPlays);
            }
        }*/

        /*[Fact]
        public void GetAllSongsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _logger);
                // Create a song
                var song = new Song
                {
                    ArtistName = "Bad Posture",
                    Genre = "Pop Punk",
                    Title = "Yellow",
                    Duration = TimeSpan.MaxValue,
                    NumberOfPlays = int.MaxValue - 1,
                    Lyrics = "Lorem ips subsciat",
                    isOriginal = true
                };

                repository.songs.Add(song);
                Assert.NotEmpty(repository.getAllSongs());
            }
        }*/

        /*[Fact]
        public async void GetListOfFriendsByUserIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _logger);

                // create a bunch of dummy users
                for (int i = 0; i < 10; i++)
                {
                    var user = new User
                    {
                        Id = int.MaxValue,
                        UserName = "jtest" + i,
                        Password = "Test1!",
                        FirstName = "Johnny",
                        LastName = "Test",
                        Email = "johnnytest123[" + i + "]@email.com"
                    };
                    repository.users.Add(user);
                }

                // make them friends
                await repository.

                Assert.NotNull(await repository.CreateNewUser(user.UserName, user.Password, user.Email));
            }
        }*/

        [Fact]
        public async Task GetMessagesViewModelTest() { }

        [Fact]
        public async Task GetLoggedInUserTest() { }
    }
}
