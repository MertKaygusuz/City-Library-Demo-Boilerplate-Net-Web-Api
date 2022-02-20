using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryDomain.Seeds
{
    class BooksSeed : IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
            var books = new Books[]
            {
                new Books
                {
                    BookId = 1,
                    BookTitle = "Ailenin, Devletin ve Özel Mülkiyetin Kökeni",
                    Author = "Friedrich Engels",
                    FirstPublishDate = DateTime.Now.AddYears(-138),
                    EditionNumber = 4,
                    EditionDate = DateTime.Now.AddYears(-120),
                    TitleType = BookTitleTypes.Science,
                    CoverType = BookCoverTypes.HardCover,
                    AvailableCount = 3,
                    ReservedCount = 1
                },
                 new Books
                 {
                     BookId = 2,
                     BookTitle = "Beyoğlu Rapsodisi",
                     Author = "Ahmet Ümit",
                     FirstPublishDate = DateTime.Now.AddYears(-19),
                     EditionNumber = 4,
                     EditionDate = DateTime.Now.AddYears(-5),
                     TitleType = BookTitleTypes.Literature,
                     CoverType = BookCoverTypes.HardCover,
                     AvailableCount = 4,
                     ReservedCount = 2
                 },
                 new Books
                  {
                      BookId = 3,
                      BookTitle = "Beyoğlu Rapsodisi",
                      Author = "Ahmet Ümit",
                      FirstPublishDate = DateTime.Now.AddYears(-19),
                      EditionNumber = 3,
                      EditionDate = DateTime.Now.AddYears(-10),
                      TitleType = BookTitleTypes.Literature,
                      CoverType = BookCoverTypes.HardCover,
                      AvailableCount = 3,
                      ReservedCount = 0
                  },
                 new Books
                  {
                      BookId = 4,
                      BookTitle = "Thomas' Calculus",
                      Author = "George Brinton Thomas",
                      FirstPublishDate = DateTime.Now.AddYears(-70),
                      EditionNumber = 13,
                      EditionDate = DateTime.Now.AddYears(-5),
                      TitleType = BookTitleTypes.Math,
                      CoverType = BookCoverTypes.SoftCover,
                      AvailableCount = 500,
                      ReservedCount = 0
                  },
                 new Books
                  {
                      BookId = 5,
                      BookTitle = "Thomas' Calculus",
                      Author = "George Brinton Thomas",
                      FirstPublishDate = DateTime.Now.AddYears(-70),
                      EditionNumber = 13,
                      EditionDate = DateTime.Now.AddYears(-5),
                      TitleType = BookTitleTypes.Math,
                      CoverType = BookCoverTypes.HardCover,
                      AvailableCount = 50,
                      ReservedCount = 0
                  }
            };

            builder.HasData(books);
        }
    }
}
