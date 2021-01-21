using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer;
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

        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void DoesUserExistTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void GetUserByNameAndPassTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void SaveUserToDbTest()
        {
        
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CreateNewUserTest() 
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void GetAllMessagesAsyncTest()
        {
        
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void GetUsersByPartialNTest()
        {
        
        }
    }
}
