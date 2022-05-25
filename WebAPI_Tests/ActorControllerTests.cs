using Microsoft.AspNetCore.Mvc.Testing;

namespace WebAPI_Tests
{
    [TestClass]
    public class ActorControllerTests
    {
        private HttpClient _httpClient;

        public ActorControllerTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task GetActorByID_WithExsitingActorID()
        {
            var response = await _httpClient.GetAsync("/Actor/23");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetActorByID_WithNonExsitingActorID()
        {
            var response = await _httpClient.GetAsync("/Actor/556848615");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task GetActorsMovies_WithExsitingActorID()
        {
            var response = await _httpClient.GetAsync("/Actor/23/movie_credits");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetActorsMovies_WithoutExsitingActorID()
        {
            var response = await _httpClient.GetAsync("/Actor/65464646/movie_credits");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task GetPopularActors_WithExsitingPage()
        {
            var response = await _httpClient.GetAsync("/Actor/popularActors?page=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetPopularActors_WithoutExsitingPage()
        {
            var response = await _httpClient.GetAsync("/Actor/popularActors?page=550");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task GetActorsSearch_WithExsitingPageAndQuery()
        {
            var response = await _httpClient.GetAsync("/Actor/search?page=1&query=zend");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetActorsSearch_WithoutExsitingPageAndQuery()
        {
            var response = await _httpClient.GetAsync("/Actor/search?page=550&query=zebbcfbcvnd");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }
    }
}
