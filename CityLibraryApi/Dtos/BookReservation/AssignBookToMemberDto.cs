using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation
{
    public class AssignBookToMemberDto
    {
        public int BookId { get; set; }

        public string UserName { get; set; }
    }
}
