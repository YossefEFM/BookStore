using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs.Book
{
    public class CreateBook
    {
        [Required(ErrorMessage = "اسم الكتاب مطلوب")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(1, 10000, ErrorMessage = "السعر يجب أن يكون بين 1 و 10000")]
        public decimal Price { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int PublishingHouseId { get; set; }
    }
}