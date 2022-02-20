using CityLibraryInfrastructure.DbBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityLibraryInfrastructure.Entities
{
    public class Roles : TableBase
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        //virtual, might be lazy loaded
        public virtual ICollection<MemberRoles> MemberRoles { get; set; }

        public virtual ICollection<Members> Members { get; set; }
    }
}
