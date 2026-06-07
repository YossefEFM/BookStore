using Microsoft.AspNetCore.Identity;

namespace BookStore.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // ضيف أي بيانات تانية محتاجها لليوزر
    }
}