namespace BookStore.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Foreign Keys & Navigation Properties
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int PublishingHouseId { get; set; }
        public PublishingHouse PublishingHouse { get; set; }
    }
}