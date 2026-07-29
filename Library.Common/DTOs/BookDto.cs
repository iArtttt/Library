namespace Library.Common.DTOs
{
    public record BookDto(Guid Id, string Name, int Count, List<string> AuthorNames);
}
