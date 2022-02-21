using CityLibraryInfrastructure.Entities.Cache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Queries.Token
{
    public record RefreshTokenGetByKeyQuery(string Key) : IRequest<RefreshTokens>
    {
    }
}
