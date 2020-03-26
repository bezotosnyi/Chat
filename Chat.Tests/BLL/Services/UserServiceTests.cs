namespace Chat.Tests.BLL.Services
{
    using System;

    using AutoMapper;

    using Chat.BLL.Infrastructure;
    using Chat.BLL.Services;
    using Chat.DAL;
    using Chat.DAL.Repositories;
    using Chat.Domain.Entities;
    using Chat.Domain.Entities.Enum;
    using Chat.DTO;
    using Chat.DTO.Response;

    using NUnit.Framework;

    [TestFixture]
    public class UserServiceTests
    {
        private IUserService userService;

        [SetUp]
        public void SetUp()
        {
            var dbContext = new ChatDbContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var userRepository = new UserRepository(dbContext);
            var loggingService = new LoggingService();
            var operationStatusService = new OperationStatusService(loggingService);
            this.userService = new UserService(unitOfWork, userRepository, loggingService, operationStatusService);

            Mapper.Initialize(
                x =>
                    {
                        x.CreateMap<User, UserDTO>();
                        x.CreateMap<UserDTO, User>();
                        x.CreateMap<User, UserLoginDTO>();
                        x.CreateMap<UserLoginDTO, User>();
                    });
        }

        [Test]
        public void RegistrationTest()
        {
            var registerUser = new UserDTO
                                   {
                                       FirstName = "Nastya",
                                       LastName = "Klimenko",
                                       MiddleName = "Vladimirovna",
                                       Gender = GenderDTO.Female,
                                       DateOfBirthday = new DateTime(1995, 11, 8),
                                       Email = "nk@mail.com",
                                       Login = "nastya",
                                       Password = "klimenko"
                                   };

            var registrationResult = this.userService.Registration(registerUser);
            Assert.AreEqual(OperationStatus.Success, registrationResult.OperationStatus);
            TestContext.Write(registrationResult.AttachedInfo);
        }

        [Test]
        public void IncorrectRegistrationTest()
        {
            var registerUser = new UserDTO
                                   {
                                       FirstName = "Nastya",
                                       LastName = "Klimenko",
                                       MiddleName = "Vladimirovna",
                                       Gender = GenderDTO.Female,
                                       DateOfBirthday = new DateTime(1995, 11, 8),
                                       Email = "nk@mail.com",
                                       Login = "admin",
                                       Password = "admin"
                                   };

            var registrationResult = this.userService.Registration(registerUser);
            Assert.AreEqual(OperationStatus.Fail, registrationResult.OperationStatus);
            TestContext.Write(registrationResult.AttachedInfo);
        }

        [Test]
        public void IncorrectRegistrationTest2()
        {
            var registerUser = new UserDTO
            {
                FirstName = "Nastya",
                LastName = "Klimenko",
                MiddleName = "Vladimirovna",
                Gender = GenderDTO.Female,
                DateOfBirthday = new DateTime(2020, 11, 8),
                Email = "nk@mail.com",
                Login = "admin",
                Password = "admin"
            };

            var registrationResult = this.userService.Registration(registerUser);
            Assert.AreEqual(OperationStatus.Fail, registrationResult.OperationStatus);
            TestContext.Write(registrationResult.AttachedInfo);
        }

        [Test]
        public void IncorrectRegistrationTest3()
        {
            var registerUser = new UserDTO
            {
                Login = "admin",
                Password = "admin"
            };

            var registrationResult = this.userService.Registration(registerUser);
            Assert.AreEqual(OperationStatus.Fail, registrationResult.OperationStatus);
            TestContext.Write(registrationResult.AttachedInfo);
        }

        [Test]
        public void LoginTest()
        {
            var userLogin = new UserLoginDTO { Login = "admin", Password = "admin" };
            var loginResult = this.userService.Login(userLogin);
            Assert.AreEqual(OperationStatus.Success, loginResult.OperationStatus);
        }

        [Test]
        public void IncorrectLoginTest2()
        {
            var userLogin = new UserLoginDTO { Login = "adm", Password = "admin" };
            var loginResult = this.userService.Login(userLogin);
            Assert.AreEqual(OperationStatus.Fail, loginResult.OperationStatus);
            TestContext.Write(loginResult.AttachedInfo);
        }

        [Test]
        public void IncorrectLoginTest3()
        {
            var userLogin = new UserLoginDTO { Login = "qwerty", Password = "qwerty" };
            var loginResult = this.userService.Login(userLogin);
            Assert.AreEqual(OperationStatus.Fail, loginResult.OperationStatus);
            TestContext.Write(loginResult.AttachedInfo);
        }
    }
}