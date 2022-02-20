using CityLibraryInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryDomain.Seeds
{
    class MemberRolesSeed : IEntityTypeConfiguration<MemberRoles>
    {
        public void Configure(EntityTypeBuilder<MemberRoles> builder)
        {
            builder.HasData(
                new MemberRoles
                {
                    Id = 1,
                    RoleId = 1,
                    MemberId = "Admin",
                },
                new MemberRoles
                {
                    Id = 2,
                    RoleId = 2,
                    MemberId = "Admin",
                },
                 new MemberRoles
                 {
                     Id = 3,
                     RoleId = 2,
                     MemberId = "User1",
                 },
                 new MemberRoles
                 {
                     Id = 4,
                     RoleId = 2,
                     MemberId = "User2",
                 },
                 new MemberRoles
                 {
                     Id = 5,
                     RoleId = 2,
                     MemberId = "User3",
                 }
            );
        }
    }
}
