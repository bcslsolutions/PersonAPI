using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PersonAPI.Business;
using PersonAPI.Data.Dto;
using PersonAPI.Models;

namespace PersonAPI.Controllers
{
    public class PersonController : ApiController
    {
        private readonly IPersonManager _personManager;

        public PersonController(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        public PersonController()
        {
            _personManager = new PersonManager();
        }

        [HttpGet]
        public PersonModel Get(int id)
        {
            try
            {
                var person = _personManager.GetPersonById(id);

                // TODO:Automapper
                var model = new PersonModel
                {
                    Age = person.Age,
                    DateOfBirth = person.DateOfBirth,
                    DateOfDeath = person.DateOfDeath,
                    FamilyName = person.FamilyName,
                    GivenName = person.GivenName,
                    Height = person.Height,
                    Id = person.Id,
                    MiddleNames = person.MiddleNames,
                    PlaceOfBirth = person.PlaceOfBirth,
                    TwitterId = person.TwitterId
                };

                return model;

            }
            catch (Exception)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)); 
            }
 
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]PersonModel personModel)
        {
            try
            {
                if (!ModelState.IsValid || personModel == null) return new HttpResponseMessage(HttpStatusCode.BadRequest);

                var person = new Person
                {
                    DateOfBirth = personModel.DateOfBirth,
                    DateOfDeath = personModel.DateOfDeath,
                    FamilyName = personModel.FamilyName,
                    GivenName = personModel.GivenName,
                    Height = personModel.Height,
                    Id = personModel.Id,
                    MiddleNames = personModel.MiddleNames,
                    PlaceOfBirth = personModel.PlaceOfBirth,
                    TwitterId = personModel.TwitterId
                };

                _personManager.AddPerson(person);

                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                 throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)); 
            }
           
        }

    }

}
