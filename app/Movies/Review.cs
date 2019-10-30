using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models {
    public class Review {
        public int Id { get; set; }

        [ScaffoldingOptions("MovieTitle", "Title")]
        public int MovieID { get; set; }

        [Display (Name = "Review Date")]
        [DataType (DataType.Date)]
        public DateTime ReviewDate { get; set; }

        [ScaffoldingOptions("", "Title")]
        public Movie Movie { get; set; }

        [Encrypted]
        [Display (Name = "Review")]
        public string Article { get; set; }
    }
}