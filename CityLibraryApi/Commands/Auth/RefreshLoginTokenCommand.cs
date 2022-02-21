using CityLibraryApi.Dtos.Token.Records;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Commands.Auth
{
    public record RefreshLoginTokenCommand(string RefreshTokenKey) : IRequest<ReturnTokenRecord>
    {
    }
}
