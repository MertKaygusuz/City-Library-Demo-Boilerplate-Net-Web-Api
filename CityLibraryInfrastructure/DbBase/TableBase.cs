using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.DbBase
{
    public abstract class TableBase : ISoftDeletable
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        [MaxLength(30)]
        public string CreatedBy { get; set; }

        [MaxLength(30)]
        public string LastUpdatedBy { get; set; }

        [MaxLength(30)]
        public string DeletedBy { get; set; }
    }
}
