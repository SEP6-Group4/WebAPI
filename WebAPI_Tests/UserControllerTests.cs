using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI.Models;

namespace WebAPI_Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private HttpClient _httpClient;

        public UserControllerTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task GetValidateUserByEmailAndPassword_WithExsitingEmailAndPassword()
        {
            var response = await _httpClient.GetAsync("/api/User/validate?email=sep%40sep.sep&password=poopoo");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetValidateUserByEmailAndPassword_WithoutExsitingEmailAndPassword()
        {
            var response = await _httpClient.GetAsync("/api/User/validate?email=sep%40sep&password=poo");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, respons);
        }

        [TestMethod]
        public async Task GetUserByUserID_WithExsitingUserID()
        {
            var response = await _httpClient.GetAsync("/api/User/get?id=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetUserByUserID_WithoutExsitingUserID()
        {
            var response = await _httpClient.GetAsync("/api/User/get?id=5");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, respons);
        }
    }
}
