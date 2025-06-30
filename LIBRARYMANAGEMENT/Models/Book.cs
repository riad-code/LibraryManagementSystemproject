using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBRARYMANAGEMENT.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string Publisher { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublishedDate { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }
       

        

        

        public string CoverImage { get; set; }

       
        

    }

}
