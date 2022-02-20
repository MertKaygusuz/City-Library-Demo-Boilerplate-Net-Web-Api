﻿using CityLibraryInfrastructure.DbBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.Entities
{
    public class BookReservationHistories : TableBase
    {
        public int HistoryId { get; set; }

        public DateTime TakenDate { get; set; } //TODO:?

        public DateTime GivenDate { get; set; } //TODO:?

        public string MemberId { get; set; }

        public Members Member { get; set; }

        public int BookId { get; set; }

        public Books Book { get; set; }
    }
}
