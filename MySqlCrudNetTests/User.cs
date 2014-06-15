using System;
using Crud;

namespace MySqlCrudNetTests
{
	public class User : ILongIdentifiable
	{
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateCreated { get; set; }
		public char Gender { get; set; }
	}
}