using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Notifications.Test
{
    public record TestNotification(string Address, string FullName) : INotification
    {
    }
}
