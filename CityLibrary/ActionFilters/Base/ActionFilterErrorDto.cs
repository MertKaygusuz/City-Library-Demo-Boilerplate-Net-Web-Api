using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.ActionFilters.Base
{
    public class ActionFilterErrorDto
    {
        public readonly short status;
        public ActionFilterErrorDto(short status = 400)
        {
            this.status = status;
        }

        public Dictionary<string, List<string>> Errors { get; set; } = new();
    }
}
