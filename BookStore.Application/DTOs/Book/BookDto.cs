namespace BookStore.Application.DTOs.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // تفاصيل العلاقات عشان نعرضها في الـ GET
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}