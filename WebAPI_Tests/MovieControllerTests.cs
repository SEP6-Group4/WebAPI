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
            var respons = response.StatusCode;

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

        [TestMethod]
        public async Task GetMoviesByRatingAsc_WithExistingPage()
        {
            var response = await _httpClient.GetAsync("/Movie/ByRatingAsc?page=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetMoviesByRatingAsc_WithNonExistingPage()
        {
            var response = await _httpClient.GetAsync("/Movie/ByRatingAsc?page=1500");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task GetMoviesByTitleDesc_WithExistingPage()
        {
            var response = await _httpClient.GetAsync("/Movie/ByTitleDesc?page=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetMoviesByTitleDesc_WithNonExistingPage()
        {
            var response = await _httpClient.GetAsync("/Movie/ByTitleDesc?page=1500");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task GetMoviesByTitleAsc_WithExistingPage()
        {
            var response = await _httpClient.GetAsync("/Movie/ByTitleAsc?page=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetMoviesByTitleAsc_WithNonExistingPage()
        {
            var response = await _httpClient.GetAsync("/Movie/ByTitleAsc?page=1500");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task GetCreditsByMovieID_WithExistingMovieID()
        {
            var response = await _httpClient.GetAsync("/Movie/45859/credits");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetCreditsByMovieID_WithoutExistingMovieID()
        {
            var response = await _httpClient.GetAsync("/Movie/1/credits");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task GetMoviesBySearch_WithExistingPageAndQuery()
        {
            var response = await _httpClient.GetAsync("/Movie/search?page=1&query=the");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetMoviesBySearch_WithoutExistingPageAndQuery()
        {
            var response = await _httpClient.GetAsync("/Movie/search?page=1500&query=thdsadasdasdgsde");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }
    }
}