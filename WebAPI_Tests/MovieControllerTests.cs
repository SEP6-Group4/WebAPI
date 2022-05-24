using Microsoft.AspNetCore.Mvc.Testing;

namespace WebAPI_Tests
{
    [TestClass]
    public class MovieControllerTests
    {
        private HttpClient _httpClient;

        public MovieControllerTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task GetMovieByID_WithExsitingMovieID()
        {
            var response = await _httpClient.GetAsync("/Movie/43875");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetMovieByID_WithNonExsitingMovieID()
        {
            var response = await _httpClient.GetAsync("/Movie/1");
            var respons =  response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task GetMoviesByRatingDesc_WithExistingPage()
        {
            var response = await _httpClient.GetAsync("/Movie/ByRatingDesc?page=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetMoviesByRatingDesc_WithNonExistingPage()
        {
            var response = await _httpClient.GetAsync("/Movie/ByRatingDesc?page=1500");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }
    }
}