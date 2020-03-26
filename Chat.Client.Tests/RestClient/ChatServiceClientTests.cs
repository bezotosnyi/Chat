namespace Chat.Client.Tests.RestClient
{
    using Chat.DTO;
    using Chat.DTO.Response;
    using Chat.FrontServiceClient;
    using Chat.FrontServiceClient.Infrastructure;
    using Chat.RestClient;

    using NUnit.Framework;

    [TestFixture]
    public class ChatServiceClientTests
    {
        private IFrontServiceClient frontServiceClient;

        [SetUp]
        public void SetUp()
        {
            this.frontServiceClient = new FrontServiceClient(new ChatServiceClient("http://localhost:8080"));
        }

        [Test]
        public void LoginTest()
        {
            var user = new UserLoginDTO { Login = "admin", Password = "admin" };
            var response = this.frontServiceClient.Login(user);
            Assert.AreEqual(OperationStatus.Success, response.OperationStatus);
            TestContext.Write(response.AttachedInfo);
        }
    }
}