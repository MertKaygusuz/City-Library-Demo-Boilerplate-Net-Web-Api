using CityLibraryInfrastructure.Entities;
using MediatR;

namespace CityLibraryApi.Queries.Member
{
    public record GetMemberByUserNameQuery(string UserName) : IRequest<Members>
    {
    }
}
