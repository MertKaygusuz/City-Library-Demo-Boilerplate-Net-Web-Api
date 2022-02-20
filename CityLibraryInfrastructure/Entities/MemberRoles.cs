using CityLibraryInfrastructure.DbBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.Entities
{
    public class MemberRoles : TableBase
    {
        public int Id { get; set; }

        public string MemberId { get; set; }

        public Members Member { get; set; }

        public int RoleId { get; set; }

        public Roles Role { get; set; }
    }
}
