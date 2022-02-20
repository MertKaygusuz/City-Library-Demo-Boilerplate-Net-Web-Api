using CityLibraryApi.Dtos.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Services.Token.Interfaces
{
    public interface IAccessTokenService
    {
        CreateTokenResultDto CreateToken(CreateTokenDto dto);
    }
}
