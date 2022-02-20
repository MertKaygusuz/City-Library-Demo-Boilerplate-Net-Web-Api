using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.BaseInterfaces
{
    public interface IBaseCheckService
    {
        Task<bool> DoesEntityExistAsync(IConvertible Id);
    }
}
