using NUnit.Framework;
using MySqlCrudNet;

namespace MySqlCrudNetTests
{
	[TestFixture]
	public class IntegrationTests
	{
		private IMySqlDbContext _dbContext;
		private IMySqlCommandProvider<User> _commandProvider;
		private IMySqlObjectBuilder<User> _objectBuilder;
		private UserRepository _userRepository;

		[SetUp]
		public void Setup()
		{
			_dbContext = new MySqlDbContext();
			_dbContext.Connect("server=localhost;userid=jestin;password=crudnet;database=crudnet_test");
			_commandProvider = new UserCommandProvider();
			_objectBuilder = new UserObjectBuilder();
			_userRepository = new UserRepository(_dbContext, _commandProvider, _objectBuilder);
		}

		[TearDown]
		public void TearDown()
		{
			_userRepository.DeleteAll();
		}

		[Test]
		public void Create()
		{
			var user = new User
			{
				FirstName = "Jestin",
				LastName = "Stoffel",
				Gender = 'M'
			};

			_userRepository.Create(user);
		}
	}
}