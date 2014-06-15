using MySqlCrudNet;
using NUnit.Framework;
using System.Linq;

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

			user = _userRepository.Create(user);

			var retrievedUser = _userRepository.Retrieve(user.Id);

			Assert.IsNotNull(retrievedUser);
			Assert.AreEqual(user.Id, retrievedUser.Id);
			Assert.AreEqual(user.FirstName, retrievedUser.FirstName);
			Assert.AreEqual(user.LastName, retrievedUser.LastName);
			Assert.AreEqual(user.Gender, retrievedUser.Gender);
		}

		[Test]
		public void Update()
		{
			var user = new User
			{
				FirstName = "Jestin",
				LastName = "Stoffel",
				Gender = 'M'
			};

			user = _userRepository.Create(user);

			user.FirstName = "Michelle";
			user.Gender = 'F';

			_userRepository.Update(user);

			var retrievedUser = _userRepository.Retrieve(user.Id);

			Assert.IsNotNull(retrievedUser);
			Assert.AreEqual(user.Id, retrievedUser.Id);
			Assert.AreEqual(user.FirstName, retrievedUser.FirstName);
			Assert.AreEqual(user.LastName, retrievedUser.LastName);
			Assert.AreEqual(user.Gender, retrievedUser.Gender);
		}

		[Test]
		public void RetrieveAll()
		{
			var user1 = new User
			{
				FirstName = "Jestin",
				LastName = "Stoffel",
				Gender = 'M'
			};

			var user2 = new User
			{
				FirstName = "Michelle",
				LastName = "Stoffel",
				Gender = 'F'
			};

			var user3 = new User
			{
				FirstName = "Zaphod",
				LastName = "Stoffel",
				Gender = 'M'
			};

			var user4 = new User
			{
				FirstName = "Zelda",
				LastName = "Stoffel",
				Gender = 'F'
			};

			_userRepository.Create(user1);
			_userRepository.Create(user2);
			_userRepository.Create(user3);
			_userRepository.Create(user4);

			var users = _userRepository.RetrieveAll();

			Assert.IsNotNull(users);
			Assert.IsNotEmpty(users);
			Assert.AreEqual(4, users.Count());
		}
	}
}