using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
    public class PublishingHouse
    {
        public int Id { get; set; }
        public string Name { get; set; } // اسم دار النشر
        public string Address { get; set; } // عنوان دار النشر

        // الـ Navigation Property:
        // دار النشر الواحدة تقدر تطبع وتنشر قائمة كبيرة من الكتب
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}