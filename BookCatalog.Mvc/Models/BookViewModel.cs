using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCatalog.Mvc.Models
{
    public class BookViewModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; } = string.Empty;
        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; } = string.Empty;
        [Required(ErrorMessage = "PageCount is required.")]
        public int PageCount { get; set; }
    }
}
