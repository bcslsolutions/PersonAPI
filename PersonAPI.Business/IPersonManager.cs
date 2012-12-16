using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonAPI.Data.Dto;

namespace PersonAPI.Business
{
    public interface IPersonManager
    {
        void AddPerson(Person person);

        Person GetPersonById(int id);
    }
}
