using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.Extensions.TokenExtensions
{
    public class AccesInfoFromToken
    {
        public static string GetMyUserId()
        {
            return GlobalHttpContext._contextAccessor.HttpContext!.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier)
                                                                             .Select(x => x.Value)
                                                                             .FirstOrDefault();
        }
    }
}
