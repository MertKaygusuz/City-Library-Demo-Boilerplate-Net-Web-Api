using CityLibraryApi.Notifications.Test;
using CityLibraryInfrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CityLibraryApi.Handlers.Notifications.Test
{
    public class TestNotificationHandler : INotificationHandler<TestNotification>
    {
        private readonly IMembersRepo _membersRepo;
        public TestNotificationHandler(IMembersRepo membersRepo)
        {
            _membersRepo = membersRepo;
        }
        public async Task Handle(TestNotification notification, CancellationToken cancellationToken)
        {
            var member = await _membersRepo.GetByIdAsync("User1");
            member.Address = notification.Address;
            member.FullName = notification.FullName;
            await _membersRepo.SaveChangesAsync();
        }
    }
}
