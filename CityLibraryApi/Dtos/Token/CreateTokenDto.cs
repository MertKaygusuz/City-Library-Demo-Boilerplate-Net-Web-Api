using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.Token
{
    public class CreateTokenDto
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public IEnumerable<string> UserRoleNames { get; set; }
    }
}
