namespace RatingService.Domain.Entities
{
	public class User
	{
		public int Id { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public Enterprise Enterprise { get; set; }
	}
}
