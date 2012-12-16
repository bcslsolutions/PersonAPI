using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using NUnit.Framework;
using PersonAPI.Business;
using PersonAPI.Controllers;
using PersonAPI.Data.Dto;
using PersonAPI.Models;
using Rhino.Mocks;

namespace PersonAPI.Tests.Controllers
{
    [TestFixture]
    public class PersonControllerTest
    {
        private IPersonManager _personManager;

        [SetUp]
        public void SetUp()
        {
            _personManager = MockRepository.GenerateMock<IPersonManager>();
        }

        [Test]
        public void Get_Assert_GetPersonById_Was_Called()
        {
            // Arrange
            var person = GenerateTestPersons(1).FirstOrDefault();
            _personManager.Stub(x => x.GetPersonById(Arg<int>.Is.Anything)).Return(person);
            var controller = new PersonController(_personManager);

            // Act
            controller.Get(1);

            // Assert
            _personManager.AssertWasCalled(x=>x.GetPersonById(Arg<int>.Is.Anything));
        }


        [Test]
        public void Get_Assert_GetPersonById_Returns_Full_Properties_On_Person()
        {
            // Arrange
            var person = GenerateTestPersons(1).FirstOrDefault();
            _personManager.Stub(x => x.GetPersonById(Arg<int>.Is.Anything)).Return(person);
            var controller = new PersonController(_personManager);

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(DateTime.Now.AddYears(-80).ToShortDateString(), result.DateOfBirth.ToShortDateString());
            Assert.AreEqual(DateTime.Now.ToShortDateString(), result.DateOfDeath.Value.ToShortDateString());
            Assert.AreEqual("FamilyName-0", result.FamilyName);
            Assert.AreEqual("GivenName-0", result.GivenName);
            Assert.AreEqual(134.9, result.Height);
            Assert.AreEqual(new string[2] { "Middle1-0", "Middle2-0" }.Count(), result.MiddleNames.Count());
            Assert.AreEqual("Middle1-0", result.MiddleNames[0]);
            Assert.AreEqual("Middle2-0", result.MiddleNames[1]);
            Assert.AreEqual("PlaceOfBirth-0", result.PlaceOfBirth);
            Assert.AreEqual("TwitterId-0", result.TwitterId);
        }


        [Test]
        public void Post_Assert_AddPerson_Was_Called()
        {
            // Arrange
            _personManager.Stub(x => x.AddPerson(Arg<Person>.Is.Anything));
            var controller = new PersonController(_personManager);

            // Act
            controller.Post(new PersonModel());

            // Assert
            _personManager.AssertWasCalled(x => x.AddPerson(Arg<Person>.Is.Anything));
        }


        [Test]
        public void Post_Assert_AddPerson_Returns_BadRequest_When_Called_With_Null_Person()
        {
            // Arrange
            _personManager.Stub(x => x.AddPerson(Arg<Person>.Is.Anything));
            var controller = new PersonController(_personManager);

            // Act
            var response = controller.Post(null);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Test]
        public void Post_Assert_AddPerson_Returns_Created_StatusCode_When_Called_With_NotNull_Person()
        {
            // Arrange
            _personManager.Stub(x => x.AddPerson(Arg<Person>.Is.Anything));
            var controller = new PersonController(_personManager);

            // Act
            var response = controller.Post(new PersonModel());

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void IntegrationTest()
        {
            // Arrange
            _personManager.Stub(x => x.AddPerson(Arg<Person>.Is.Anything));
            

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/products");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "person" } });
            var controller = new PersonController(_personManager);
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            // Act
            var result = controller.Post(new PersonModel { Id = 1 });

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);


        }


        private IEnumerable<Person> GenerateTestPersons(int count)
        {
            var returnList = new List<Person>();

            for (var i = 0; i < count; i++)
            {
                returnList.Add(new Person
                {
                    Id = i,
                    DateOfBirth = DateTime.Now.AddYears(-80),
                    DateOfDeath = DateTime.Now.AddYears(-i),
                    FamilyName = "FamilyName-" + i,
                    GivenName = "GivenName-" + i,
                    Height = 134.9 + i,
                    MiddleNames = new string[2] { "Middle1-" + i, "Middle2-" + i },
                    PlaceOfBirth = "PlaceOfBirth-" + i,
                    TwitterId = "TwitterId-" +i
                });

            }

            return returnList;
        }

    }
}
