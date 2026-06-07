using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } // اسم القسم (مثلاً: رعب، تكنولوجيا)

        // الـ Navigation Property:
        // القسم الواحد (زي الرعب مثلاً) جواه قائمة من الكتب الكثيرة تنتمي للقسم ده
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}