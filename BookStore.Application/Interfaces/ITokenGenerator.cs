using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}
