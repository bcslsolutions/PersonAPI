using System;
using NUnit.Framework;
using PersonAPI.Data;
using PersonAPI.Data.Dto;
using Rhino.Mocks;

namespace PersonAPI.Business.Tests
{
    [TestFixture]
    public class PersonManagerTests
    {
        private IDataManager _dataManager;

        [SetUp]
        public void SetUp()
        {
            _dataManager = MockRepository.GenerateMock<IDataManager>();
        }

        [Test]
        public void GetPersonById_Assert_GetPersonFromRepositoryById_Was_Called()
        {
            // Arrange
            _dataManager.Stub(x => x.GetPersonFromRepositoryById(Arg<int>.Is.Anything)).Return(new Person());
            var personManager = new PersonManager(_dataManager);

            // Act
            personManager.GetPersonById(1);
            
            // Assert
            _dataManager.AssertWasCalled(x => x.GetPersonFromRepositoryById(Arg<int>.Is.Anything));
        }

        [Test]
        public void AddPerson_Assert_AddPersonToRepository_Was_Called()
        {
            // Arrange
            _dataManager.Stub(x => x.AddPersonToRepository(Arg<Person>.Is.Anything));
            var personManager = new PersonManager(_dataManager);

            // Act
            personManager.AddPerson(new Person());

            // Assert
            _dataManager.AssertWasCalled(x => x.AddPersonToRepository(Arg<Person>.Is.Anything));

        }


        [Test]
        public void GetPersonById_Assert_Person_Returned()
        {
            // Arrange
            var expected = new Person();
            _dataManager.Stub(x => x.GetPersonFromRepositoryById(Arg<int>.Is.Anything)).Return(expected);
            var personManager = new PersonManager(_dataManager);

            // Act
            var actual = personManager.GetPersonById(1);

            // Assert
            Assert.AreEqual(expected,actual);

        }

        [Test]
        public void GetPersonById_Assert_Person_Returned_With_Correct_Age()
        {
            // Arrange
            var expected = new Person{DateOfBirth = DateTime.Now.AddYears(-30)};
            _dataManager.Stub(x => x.GetPersonFromRepositoryById(Arg<int>.Is.Anything)).Return(expected);
            var personManager = new PersonManager(_dataManager);

            // Act
            var actual = personManager.GetPersonById(1);

            // Assert
            Assert.AreEqual(30, actual.Age);

        }

    }
}
