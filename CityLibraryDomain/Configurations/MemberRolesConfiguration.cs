using CityLibraryInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryDomain.Configurations
{
    //TODO: delete file
    internal class MemberRolesConfiguration //: IEntityTypeConfiguration<MemberRoles>
    {
        public void Configure(EntityTypeBuilder<MemberRoles> builder)
        {
            builder.HasKey(mr => new { mr.RoleId, mr.MemberId });
            builder.HasOne(mr => mr.Role)
                .WithMany(r => r.MemberRoles)
                .HasForeignKey(mr => mr.RoleId);
            builder.HasOne(mr => mr.Member)
                .WithMany(r => r.MemberRoles)
                .HasForeignKey(mr => mr.MemberId);
        }
    }
}
