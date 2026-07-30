namespace Library.Common.Interfaces.DAL
{
	public interface IUser : IID
    {
		public string Login { get; set; }
		public string PasswordHash { get; set; }
		public string? Email { get; set; }
	}
}
