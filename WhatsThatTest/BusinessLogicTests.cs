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
        public void RequesFriendTest()
        {

        }
    }
}
