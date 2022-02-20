using CityLibraryApi.Dtos.Member;
using CityLibraryInfrastructure.BaseInterfaces;
using CityLibraryInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Services.Member.Interfaces
{
    public interface IMemberService : IBaseCheckService
    {
        Task<Members> GetMemberByUserNameAsync(string userName);

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="registrationDto"></param>
        /// <returns>User name</returns>
        Task<string> RegisterAsync(RegistrationDto registrationDto);

        /// <summary>
        /// User can be update its own information by using this method.
        /// </summary>
        /// <param name="selfUpdateDto"></param>
        /// <returns></returns>
        Task MemberSelfUpdateAsync(MemberSelfUpdateDto selfUpdateDto);

        /// <summary>
        /// Updates any user's information. Only admin must be allowed to run this method.
        /// </summary>
        /// <param name="registrationDto">Includes UserName and update parameters. UserName could not be updated.</param>
        /// <returns></returns>
        Task AdminUpdateMemberAsync(RegistrationDto registrationDto);
    }
}
