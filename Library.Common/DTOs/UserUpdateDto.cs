using Library.Common.Enums;

namespace Library.Common.DTOs
{
    public record UserUpdateDto
    (
    string Name,
    string LastName,
    string? Email,
    string? DocumentNumber,
    DocumentType? DocumentType,
    DateTime? Birthday
    );
}
