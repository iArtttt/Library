using Library.Common.Enums;

namespace Library.Common.DTOs
{
    public record UserDto(
        Guid Id,
        string Login,
        string Name,
        string LastName,
        string? Email,
        Role Role,
        string? DocumentNumber,
        DocumentType? DocumentType,
        string? Birthday
    );
}
