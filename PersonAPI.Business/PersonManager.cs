using System;
using PersonAPI.Data;
using PersonAPI.Data.Dto;

namespace PersonAPI.Business
{
    public class PersonManager : IPersonManager
    {
        private readonly IDataManager _dataManager;

        public PersonManager()
        {
            _dataManager = new DataManager();
        }

        public PersonManager(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public void AddPerson(Person person)
        {
            _dataManager.AddPersonToRepository(person);
        }

        public Person GetPersonById(int id)
        {
            var person = _dataManager.GetPersonFromRepositoryById(id);

            person.Age = CalculatePersonAge(person.DateOfBirth);

            return person;
        }

        private int CalculatePersonAge(DateTime dateOfBirth)
        {
            return DateTime.Now.Year - dateOfBirth.Year;
        }
    }
}
