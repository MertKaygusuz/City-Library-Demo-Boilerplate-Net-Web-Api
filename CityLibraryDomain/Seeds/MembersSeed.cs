using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryDomain.Seeds
{
    class MembersSeed : IEntityTypeConfiguration<Members>
    {
        public void Configure(EntityTypeBuilder<Members> builder)
        {
            string sharedPassword = "1234567890";
            sharedPassword.CreatePasswordHash(out string hashedPass);
            var members = new Members[]
            {
                new Members
                {
                     UserName = "Admin",
                     FullName = "Admin",
                     BirthDate = DateTime.Now.AddYears(-30),
                     Address = "Admin's Address",
                     Password = hashedPass
                },
                new Members
                {
                     UserName = "User1",
                     FullName = "Orhan",
                     BirthDate = DateTime.Now.AddYears(-30),
                     Address = "Orhan's Address",
                     Password = hashedPass
                },
                new Members
                {
                    UserName = "User2",
                    FullName = "Kaya",
                    BirthDate = DateTime.Now.AddYears(-40),
                    Address = "Kaya's Address",
                    Password = hashedPass
                },
                new Members
                {
                    UserName = "User3",
                    FullName = "Kadriye",
                    BirthDate = DateTime.Now.AddYears(-20),
                    Address = "Kadriye's Address",
                    Password = hashedPass
                }
            };

            builder.HasData(members);
        }
    }
}
