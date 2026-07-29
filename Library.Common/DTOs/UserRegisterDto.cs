using Library.Common.Enums;

namespace Library.Common.DTOs
{
    public record UserRegisterDto(
        string Login,
        string Password,
        string Name,
        string LastName,
        string? Email,
        Role Role,
        string? DocumentNumber,
        DocumentType? DocumentType,
        DateTime? Birthday
    );
}
