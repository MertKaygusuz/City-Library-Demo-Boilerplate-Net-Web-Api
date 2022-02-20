using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.Book
{
    public class UpdateBookDto : RegisterBookDto
    {
        //this annotations are required for GenericNotFoundFilter
        [Key]
        [DisplayName("Book")]
        public int BookId { get; set; }
    }
}
