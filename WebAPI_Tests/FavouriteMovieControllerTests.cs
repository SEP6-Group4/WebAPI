using Microsoft.AspNetCore.Mvc.Testing;

namespace WebAPI_Tests
{
    [TestClass]
    public class FavouriteMovieControllerTests
    {

        private HttpClient _httpClient;

        public FavouriteMovieControllerTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task PostAddMovieToUserFavList_WithExsitingUserIDAndMovieID()
        {
            var response = await _httpClient.PostAsync("/api/FavoriteMovie/addFavorite?userID=22&movieID=53452", null);
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task PostAddMovieToUserFavList_WithoutExsitingUserIDAndMovieID()
        {
            var response = await _httpClient.PostAsync("/api/FavoriteMovie/addFavorite?userID=5&movieID=1", null);
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task GetFavMoviesByUserID_WithExsitingUserID()
        {
            var response = await _httpClient.GetAsync("/api/FavoriteMovie/getFavorites?userID=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavMoviesByUserID_WithoutExsitingUserID()
        {
            var response = await _httpClient.GetAsync("/api/FavoriteMovie/getFavorites?userID=5");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavMoviesByUserEmail_WithExsitingUserEmail()
        {
            var response = await _httpClient.GetAsync("/api/FavoriteMovie/getFavoritesByEmail?email=sep%40sep.sep");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavMoviesByUserEmail_WithoutExsitingUserEmail()
        {
            var response = await _httpClient.GetAsync("/api/FavoriteMovie/getFavoritesByEmail?email=sep%40sep");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, respons);
        }

        [TestMethod]
        public async Task GetFavMoviesCountByMovieID_WithExsitingMovieID()
        {
            var response = await _httpClient.GetAsync("/api/FavoriteMovie/getFavoriteMovieCount?movieID=245654");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavMoviesCountByMovieID_WithoutExsitingMovieID()
        {
            var response = await _httpClient.GetAsync("/api/FavoriteMovie/getFavoriteMovieCount?movieID=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavMoviesByAgeGroup_WithExsitingAgeGroup()
        {
            var response = await _httpClient.GetAsync("/api/FavoriteMovie/getFavoriteMovieIdsByAgeGroup?ageGroup=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavMoviesByAgeGroup_WithoutExsitingAgeGroup()
        {
            var response = await _httpClient.GetAsync("/api/FavoriteMovie/getFavoriteMovieIdsByAgeGroup?ageGroup=7");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task DeleteFavMovieByUserID_WithExsitingUserIDAndMovieID()
        {
            var response = await _httpClient.DeleteAsync("/api/FavoriteMovie/removeFavorite?userID=1&movieID=53452");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task DeleteFavMovieByUserID_WithoutExsitingUserIDAndMovieID()
        {
            var response = await _httpClient.DeleteAsync("/api/FavoriteMovie/removeFavorite?userID=5&movieID=1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }
    }
}
