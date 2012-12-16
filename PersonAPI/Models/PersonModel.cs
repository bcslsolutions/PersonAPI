using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonAPI.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string[] MiddleNames { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string PlaceOfBirth { get; set; }
        public double? Height { get; set; }
        public string TwitterId { get; set; }

	    public int Age{ get; set; }

    }
}