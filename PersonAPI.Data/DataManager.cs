using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PersonAPI.Data.Dto;

namespace PersonAPI.Data
{
    public class DataManager : IDataManager
    {
        private List<Person> _personMockDataStore = new List<Person>();

        private readonly string _filePath = System.Web.HttpContext.Current.Server.MapPath("/DataStore/Person");

 
        public void AddPersonToRepository(Person person)
        {
            _personMockDataStore.Add(person);
        }


        public Person GetPersonFromRepositoryById(int id)
        {
            var personFilePath = GetPersonFilePathById(id);

            return ParsePersonFromFile(personFilePath);
        }


        public Person ParsePersonFromFile(string filepath)
        {
            var lines = ParseLinesFromFile(filepath);

            var kvp = ParseKeyValuesFromFileLines(lines);

            return MapKeyValuesToPerson(kvp);
        }

        private string GetPersonFilePathById(int id)
        {
            var files = from file in Directory.EnumerateFiles(_filePath, "*.txt", SearchOption.AllDirectories)
                        from line in File.ReadLines(file)
                        where line.Contains("id=" + id)
                        select new { File = file };

            return files.Count() > 0 ? files.FirstOrDefault().File: "";
        }

        private IEnumerable<string> ParseLinesFromFile(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);

            return from line in lines select line;
        }


        private Dictionary<string, string> ParseKeyValuesFromFileLines(IEnumerable<string> lines)
        {
            return lines.Select(str => str.Split('=')).ToDictionary(arr => arr[0], arr => arr[1]);
        }


        private static Person MapKeyValuesToPerson(Dictionary<string, string> kvp)
        {
            var person = new Person();

            foreach (var kv in kvp)
            {
                switch (kv.Key)
                {
                    case "Id":
                        person.Id = int.Parse(kv.Value);
                        break;

                    case "familyName":
                        person.FamilyName = kv.Value;
                        break;

                    case "givenName":
                        person.GivenName = kv.Value;
                        break;

                    case "middleNames":
                        person.MiddleNames = kv.Value.Split(' ');
                        break;

                    case "dateOfBirth":
                        person.DateOfBirth = DateTime.Parse(kv.Value);
                        break;

                    case "dateOfDeath":
                        if (!string.IsNullOrEmpty(kv.Value)) person.DateOfDeath = DateTime.Parse(kv.Value);
                        break;

                    case "placeOfBirth":
                        person.PlaceOfBirth = kv.Value;
                        break;

                    case "height":
                        if (!string.IsNullOrEmpty(kv.Value)) person.Height = double.Parse(kv.Value);
                        break;

                    case "twitterId":
                        person.TwitterId = kv.Value;
                        break;
                }

            }

            return person;
        }


    }
}
