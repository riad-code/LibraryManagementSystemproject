using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIBRARYMANAGEMENT.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public ICollection<Book> Books { get; set; }
    }

}
