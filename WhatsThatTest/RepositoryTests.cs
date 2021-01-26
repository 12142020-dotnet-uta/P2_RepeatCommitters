using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void GetAllUsersAsyncTest()
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
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                repository.users.Add(user);
                var listOfUsers = repository.GetAllUsersAsync();
                Assert.NotNull(listOfUsers);
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

                Repository repository = new Repository(context, _logger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                repository.users.Add(user);
                var userById = repository.GetUserByIdAsync(int.MaxValue);
                Assert.NotNull(userById);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void DoesUserExistTest()
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
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                repository.users.Add(user);
                var userExists = repository.DoesUserExist("jtest", "Test1!");
                Assert.NotNull(userExists);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void GetUserByNameAndPassTest()
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
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                repository.users.Add(user);
                var userByNameAndPass = repository.GetUserByNameAndPass("jtest", "Test1!");
                Assert.NotNull(userByNameAndPass);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async void SaveUserToDbTest()
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
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                repository.users.Add(user);
                await repository.SaveNewUser(user);
                await repository.GetUserByIdAsync(int.MaxValue);

                // edited user
                var editedUser = new User
                {
                    Id = int.MaxValue,
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
        public async void CreateNewUserTest()
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
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Assert.NotNull(await repository.CreateNewUser(user.UserName, user.Password, user.Email));
            }
        }

        [Fact]
        public async void AddSongToFavoritesTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _logger);
                // Create a new user
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };
                // Create a song to favorite
                var song = new Song
                {
                    Id = int.MaxValue,
                    ArtistName = "Bad Posture",
                    Genre = "Pop Punk",
                    Title = "Yellow",
                    Duration = TimeSpan.MaxValue,
                    NumberOfPlays = int.MaxValue,
                    Lyrics = "Lorem ips subsciat",
                    isOriginal = true
                };
                // attempt to add song to favorite list
                await repository.AddSongToFavorites(song.Id, user.Id);
                // get list of users favorite songs
                Assert.NotNull(await repository.GetUsersFavorites(user.Id));
            }
        }

        //[Fact]
        //public void GetUsersFavoritesTest() { }

        /*[Fact]
        public async void GetSongByIdTest()
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
                    Id = int.MaxValue,
                    ArtistName = "Bad Posture",
                    Genre = "Pop Punk",
                    Title = "Yellow",
                    Duration = TimeSpan.MaxValue,
                    NumberOfPlays = int.MaxValue,
                    Lyrics = "Lorem ips subsciat",
                    isOriginal = true
                };

                repository.songs.Add(song);
                Assert.NotNull(await repository.GetSongById(song.Id));
            }
        }*/

        [Fact]
        public void GetMessagesToUsersTest() { }

        [Fact]
        public void GetSongsBySearchGenreTest() { }

        [Fact]
        public async void GetTop5OriginalsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

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
                        Id = int.MaxValue - i,
                        ArtistName = "Bad Posture",
                        Genre = "Pop Punk",
                        Title = "Yellow",
                        Duration = TimeSpan.MaxValue,
                        NumberOfPlays = int.MaxValue,
                        Lyrics = "Lorem ips subsciat",
                        isOriginal = true
                    };

                    repository.songs.Add(song);
                }

                List<Song> listOf5Songs = await repository.GetTop5Originals();
                Assert.Equal(5, listOf5Songs.Count);
            }
        }

        /*[Fact]
        public async void IncrementNumPlaysTest()
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
                    Id = int.MaxValue,
                    ArtistName = "Bad Posture",
                    Genre = "Pop Punk",
                    Title = "Yellow",
                    Duration = TimeSpan.MaxValue,
                    NumberOfPlays = int.MaxValue - 1,
                    Lyrics = "Lorem ips subsciat",
                    isOriginal = true
                };

                repository.songs.Add(song);
                await repository.IncrementNumPlays(song.Id);
                Assert.Equal(int.MaxValue, song.NumberOfPlays);
            }
        }*/

        /*[Fact]
        public async void GetAllSongsTest()
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
                    Id = int.MaxValue,
                    ArtistName = "Bad Posture",
                    Genre = "Pop Punk",
                    Title = "Yellow",
                    Duration = TimeSpan.MaxValue,
                    NumberOfPlays = int.MaxValue - 1,
                    Lyrics = "Lorem ips subsciat",
                    isOriginal = true
                };

                repository.songs.Add(song);
                Assert.NotEmpty(await repository.getAllSongs());
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
        public void GetMessagesViewModelTest() { }

        [Fact]
        public void GetLoggedInUserTest() { }
    }
}
