namespace Chat.Tests.BLL.Validations
{
    using System;

    using Chat.BLL.Validations;
    using Chat.DTO;

    using NUnit.Framework;

    [TestFixture]
    public class UserValidatorTests
    {
        private UserValidator userValidator;

        [SetUp]
        public void SetUp()
        {
            this.userValidator = new UserValidator();
        }

        [Test]
        public void ValidTest()
        {
            var userLogin = new UserDTO();
            userLogin.FirstName = "dima";
            userLogin.LastName = "bezotosnyi";
            userLogin.DateOfBirthday = new DateTime(2020, 3, 20);
            userLogin.Email = "test@mail.com";
            userLogin.Login = "admin";
            userLogin.Password = "admin";
            var validationResult = this.userValidator.Validate(userLogin);
            Assert.True(validationResult.IsValid);
            TestContext.Write(validationResult.ToString());
        }

        [Test]
        public void IncorrectDateOfBirthdayTest()
        {
            var userLogin = new UserDTO();
            userLogin.FirstName = "dima";
            userLogin.LastName = "bezotosnyi";
            userLogin.DateOfBirthday = DateTime.Now;
            userLogin.Email = "test@mail.com";
            userLogin.Login = "admin";
            userLogin.Password = "admin";
            var validationResult = this.userValidator.Validate(userLogin);
            Assert.False(validationResult.IsValid);
            TestContext.Write(validationResult.ToString());
        }

        [Test]
        [TestCase("a@mailcom")]
        [TestCase("test.mail.com")]
        [TestCase("test.@mail.com")]
        public void IncorrectEmailTest(string email)
        {
            var userLogin = new UserDTO();
            userLogin.FirstName = "dima";
            userLogin.LastName = "bezotosnyi";
            userLogin.DateOfBirthday = new DateTime(2020, 3, 20);
            userLogin.Email = email;
            userLogin.Login = "admin";
            userLogin.Password = "admin";
            var validationResult = this.userValidator.Validate(userLogin);
            Assert.False(validationResult.IsValid);
        }

        [Test]
        public void IncorrectLoginTest()
        {
            var userLogin = new UserDTO();
            userLogin.Login = "adm";
            userLogin.Password = "admin";
            var validationResult = this.userValidator.Validate(userLogin);
            Assert.False(validationResult.IsValid);
            TestContext.Write(validationResult.ToString());
        }
    }
}