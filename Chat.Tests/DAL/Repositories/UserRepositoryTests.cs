namespace Chat.Tests.DAL.Repositories
{
    using System;
    using System.Linq;

    using Chat.DAL;
    using Chat.DAL.Infrastructure;
    using Chat.Domain.Entities;
    using Chat.Domain.Entities.Enum;

    using NUnit.Framework;

    [TestFixture]
    public class UserRepositoryTests
    {
        private IUnitOfWork unitOfWork;

        [SetUp]
        public void SetUp() => this.unitOfWork = new UnitOfWork();

        [Test]
        public void AddUserTest()
        {
            var user = new User
                           {
                               FirstName = "Dmytro",
                               LastName = "Bezotosnyi",
                               MiddleName = "Olegovich",
                               Gender = Gender.Male,
                               DateOfBirthday = new DateTime(1995, 8, 10),
                               Email = "test@mail.com",
                               Login = "admin",
                               Password = "admin"
                           };

            this.unitOfWork.UserRepository.Insert(user);
            this.unitOfWork.Commit();
            var first = this.unitOfWork.UserRepository.Get()?.FirstOrDefault(u => u.Login == "admin");
            Assert.AreEqual("admin", first?.Login);
        }
    }
}