using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CityLibraryApi.Dtos.Member
{
    public class RegistrationDto
    {
        [Key]
        [DisplayName("Member")]
        public string UserName { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }
    }
}
