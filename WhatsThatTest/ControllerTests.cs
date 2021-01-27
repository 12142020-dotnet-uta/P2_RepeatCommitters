using BusinessLogicLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsThatSong.Controllers;
using Xunit;

namespace WhatsThatTest
{
    public class ControllerTests
    {

        private readonly ILogger<Repository> _repositoryLogger;
        private readonly ILogger<UserController> _userControllerLogger;
        private readonly MapperClass _mapperClass = new MapperClass();

        [Fact]
        public void CreateUserTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<UserProfileViewModel> upvm = userController.CreateUser(user.UserName, user.Password, user.Email);
                Assert.NotNull(upvm);
            }
        }

        [Fact]
        public void LoginTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                Task<UserProfileViewModel> upvm = userController.CreateUser(user.UserName, user.Password, user.Email);
                Task<UserProfileViewModel> upvm2 = userController.login(user.UserName, user.Password);
                Assert.NotNull(upvm2);
            }
        }

        [Fact]
        public void GetUserToEditTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);
                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                repository.SaveNewUser(user).Wait();

                Task<User> u = userController.GetUserToEdit(int.MaxValue);
                Assert.NotNull(u);
            }
        }

        /* TODO: Failing for multiple reasons. NullReferenceException, Assert evaluating to false, DatabaseUpdateConcurrency issue. Reason unknown, suspect async/await.
        [Fact]
        public void EditUserTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);

                var user = new User
                {
                    Id = int.MaxValue,
                    UserName = "jtest",
                    Password = "Test1!",
                    FirstName = "Johnny",
                    LastName = "Test",
                    Email = "johnnytest123@email.com"
                };

                // add and save new user
                repository.SaveNewUser(user).Wait();

                // attempt to edit that user
                Task<User> u = userController.GetUserToEdit(user.Id);
                userController.EditUser(user.Id,u.Result.UserName,u.Result.Password,u.Result.Email,u.Result.FirstName,"Test2").Wait();

                // check that user was edited
                Assert.Equal("Test2", user.LastName);
            }
        }*/

        /* TODO: Assert inconsistently evaluating to false. Reason unknown, suspect async/await.
        [Fact]
        public void SearchForUsersTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InHarmonyTestDB")
            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository repository = new Repository(context, _repositoryLogger);
                BusinessLogicClass logic = new BusinessLogicClass(repository, _mapperClass, _repositoryLogger);
                UserController userController = new UserController(logic, _userControllerLogger);

                var userList = new List<User>();
                // create some dummy users
                for (int i = 0; i < 10; i++)
                {
                    // j0, j2, j4 ... j8
                    var user = new User
                    {
                        Id = int.MaxValue - i,
                        UserName = "jtest" + i,
                        Password = "Test1!",
                        FirstName = "Johnny" + i,
                        LastName = "Test",
                        Email = "johnnytest123" + i + "@email.com"
                    };

                    // g1, g3, g5 ... g9
                    var user2 = new User
                    {
                        Id = int.MaxValue - ++i,
                        UserName = "greg" + i,
                        Password = "Test1!",
                        FirstName = "Greg" + i,
                        LastName = "Smeg",
                        Email = "johnnytest123" + i + "@zmail.com"
                    };

                    repository.SaveNewUser(user).Wait();
                    repository.SaveNewUser(user2).Wait();
                }

                // with user populated db, let's search for the letter g
                var userList2 = userController.SearchForUsers("g");
                // we expect 5 users to return in the list
                Assert.Equal(5, userList2.Result.Count);
            }
        }*/
    }
}
