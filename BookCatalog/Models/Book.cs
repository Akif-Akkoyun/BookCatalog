using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCatalog.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Title must be between 10 and 200 characters.")]
        public string Title { get; set; } = default!;
        [Required(ErrorMessage = "Author field cannot be empty.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Author must be between 2 and 200 characters.")]
        public string Author { get; set; } = default!;
        [Required(ErrorMessage = "Genre field cannot be empty.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Genre must be between 2 and 200 characters.")]
        public string Genre { get; set; } = default!;
        [Required(ErrorMessage = "Page count cannot be empty.")]
        [Range(1, int.MaxValue, ErrorMessage = "Page count must be at least 1.")]
        public int? PageCount { get; set; }
    }    
}
