using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; } // المعرف الفريد للمؤلف
        public string Name { get; set; } // اسم المؤلف
        public string Bio { get; set; } // نبذة عن المؤلف

        // الـ Navigation Property:
        // بنقول لـ Entity Framework إن المؤلف الواحد عنده قائمة (Collection) من الكتب
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}