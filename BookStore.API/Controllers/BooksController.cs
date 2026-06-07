using BookStore.Application.DTOs.Book;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // 1️⃣ GET: api/Books (جلب كل الكتب بشرط تحويلها لـ BookDto)
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _unitOfWork.Repository<Book>().GetAllAsync();

            // تحويل الـ Entities لـ DTOs قبل ما نرجعها
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Price = b.Price,
                AuthorId = b.AuthorId,
                CategoryId = b.CategoryId
                // ملحوظة: أسماء المؤلفين هتظهر لو عامل Include للـ العلاقات، هنظبطها لاحقاً
            }).ToList();

            return Ok(bookDtos);
        }

        // 2️⃣ GET: api/Books/{id} (جلب كتاب واحد بالـ id)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(id);
            if (book == null) return NotFound($"كتاب برقم {id} غير موجود");

            var dto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                AuthorId = book.AuthorId,
                CategoryId = book.CategoryId
            };

            return Ok(dto);
        }

        // 3️⃣ POST: api/Books (إضافة كتاب - الـ Parameter هنا هو كلاس CreateBook بتاعك)
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBook model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var book = new Book
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                AuthorId = model.AuthorId,
                CategoryId = model.CategoryId,
                PublishingHouseId = model.PublishingHouseId
            };

            await _unitOfWork.Repository<Book>().AddAsync(book);
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = "تم إضافة الكتاب بنجاح!", BookId = book.Id });
        }

        // 4️⃣ PUT: api/Books/{id} (تعديل كتاب - بنستخدم برضه CreateBook للبيانات المأخوذة من الـ Body)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] CreateBook model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(id);
            if (book == null) return NotFound($"كتاب برقم {id} غير موجود");

            book.Title = model.Title;
            book.Description = model.Description;
            book.Price = model.Price;
            book.AuthorId = model.AuthorId;
            book.CategoryId = model.CategoryId;
            book.PublishingHouseId = model.PublishingHouseId;

            _unitOfWork.Repository<Book>().Update(book);
            await _unitOfWork.CompleteAsync();

            return Ok("تم تحديث بيانات الكتاب بنجاح");
        }

        // 5️⃣ DELETE: api/Books/{id} (حذف كتاب)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(id);
            if (book == null) return NotFound($"كتاب برقم {id} غير موجود");

            _unitOfWork.Repository<Book>().Delete(book);
            await _unitOfWork.CompleteAsync();

            return Ok("تم حذف الكتاب بنجاح");
        }
    }
}