using System;

namespace MySqlCrudNetTests
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateCreated { get; set; }
		public char Gender { get; set; }
	}
}