using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Commands.Token
{
    public record RefreshTokenDeleteCommand(string Key) : IRequest
    {

    }
}
