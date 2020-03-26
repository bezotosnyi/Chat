namespace Chat.Tests.BLL.Validations
{
    using Chat.BLL.Validations;
    using Chat.DTO;

    using NUnit.Framework;

    [TestFixture]
    public class UserLoginValidatorTests
    {
        private UserLoginValidator userLoginValidator;

        [SetUp]
        public void SetUp()
        {
            this.userLoginValidator = new UserLoginValidator();
        }

        [Test]
        public void ValidTest()
        {
            var userLogin = new UserLoginDTO { Login = "admin", Password = "admin" };
            var validationResult = this.userLoginValidator.Validate(userLogin);
            Assert.True(validationResult.IsValid);
            TestContext.Write(validationResult.ToString());
        }

        [Test]
        public void IncorrectLoginTest()
        {
            var userLogin = new UserLoginDTO { Login = null, Password = "admin" };
            var validationResult = this.userLoginValidator.Validate(userLogin);
            Assert.False(validationResult.IsValid);
            TestContext.Write(validationResult.ToString());
        }

        [Test]
        public void IncorrectPasswordTest()
        {
            var userLogin = new UserLoginDTO { Login = "admin", Password = "adm" };
            var validationResult = this.userLoginValidator.Validate(userLogin);
            Assert.False(validationResult.IsValid);
            TestContext.Write(validationResult.ToString());
        }
    }
}