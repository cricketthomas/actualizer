using System;
using Xunit;
using actualizer.Controllers;
using actualizer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using actualizer.Infastructure.Data.Actualizer.db;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace acutalizer_tests {


    public class UnitTest1 {


        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly ActualizerContext _db;

        [Fact]
        public void TestSearchComments() {
            // Arrange
            var controller = new CommentsController(configuration: _configuration, cache: _cache, db: _db);

            //Act
            var result = controller.GetSearchComments(search: "love", video_id: "SsKT0s5J8ko").Result;

            Assert.IsType<OkResult>(result);
            //var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            // Assert
            //Assert.IsType<OkResult>(result);
            //Assert.IsType<SerializableError>(badRequestResult.Value);



        }

        //[Fact]
        //public void BadRequestSearchComments() {

        //    var controller = new CommentsController(configuration: _configuration, cache: _cache, db: _db);
        //    var results = controller.GetSearchComments(search: "", video_id: "");
        //    Assert.IsType<NotFoundResult>(results);

        //}
        [Fact]
        public async void TestSuccessAwait() {
            var controller = new CommentsController(configuration: _configuration, cache: _cache, db: _db);
            var result = await controller.GetSearchComments(search: "love", video_id: "SsKT0s5J8ko");
            Assert.NotNull(result);
        }



    }
}
