using Microsoft.AspNetCore.Mvc.Testing;

namespace WebAPI_Tests
{
    [TestClass]
    public class FavouriteActorControllerTests
    {
        private HttpClient _httpClient;

        public FavouriteActorControllerTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task PostAddActorToUserFavList_WithExsitingActorIDAndUserID()
        {
            var response = await _httpClient.PostAsync("/FavouriteActor/AddActorToFavourite/1/543", null);
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task PostAddActorToUserFavList_WithoutExsitingActorIDAndUserID()
        {
            var response = await _httpClient.PostAsync("/FavouriteActor/AddActorToFavourite/5/99999", null);
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, respons);
        }

        [TestMethod]
        public async Task DeleteActorFromUserFavList_WithExsitingActorIDAndUserID()
        {
            var response = await _httpClient.DeleteAsync("/FavouriteActor/RemoveActorFromFavourite/1/543");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task DeleteActorFromUserFavList_WithoutExsitingActorIDAndUserID()
        {
            var response = await _httpClient.DeleteAsync("/FavouriteActor/RemoveActorFromFavourite/1/543");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavActorIdsByUserID_WithExsitingUserID()
        {
            var response = await _httpClient.GetAsync("/FavouriteActor/GetFavouriteActorIds/1");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavActorIdsByUserID_WithoutExsitingUserID()
        {
            var response = await _httpClient.GetAsync("/FavouriteActor/GetFavouriteActorIds/5");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavActorIdsByUserEmail_WithExsitingUserEmail()
        {
            var response = await _httpClient.GetAsync("/FavouriteActor/GetFavouriteActorIdsByEmail/sep%40sep.sep");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.OK, respons);
        }

        [TestMethod]
        public async Task GetFavActorIdsByUserEmail_WithoutExsitingUserEmail()
        {
            var response = await _httpClient.GetAsync("/FavouriteActor/GetFavouriteActorIdsByEmail/sep%40sep");
            var respons = response.StatusCode;

            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, respons);
        }
    }
}
