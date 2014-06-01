using System;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MongoCrudNet.Mongo
{
    /// <summary>
    /// This class provides CRUD repositories access to MongoDB.
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        /// <summary>
        /// The MongoDB server to which this context connects
        /// </summary>
        public virtual MongoServer Server { get; set; }

        /// <summary>
        /// The MongoDB database to which this context connects
        /// </summary>
        public virtual MongoDatabase Database { get; set; }

        /// <summary>
        /// Whether or not the connection to the database has been initialized
        /// </summary>
        public virtual bool Initialized { get; set; }

        /// <summary>
        /// Connects to a specified server and database, with any parameters passed
        /// in by the connection string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public virtual bool Connect(string connectionString, string databaseName)
        {
            Initialized = false;

            var client = new MongoClient(connectionString);
            Server = client.GetServer();

            if (Server != null)
            {
                Database = Server.GetDatabase(databaseName);

                if (Database != null)
                {
                    EnsureIndexes();
					SetUpClassMappings();
                    Initialized = true;
                }
            }

            return Initialized;
        }

        /// <summary>
        /// This is a method that abstracts registering classes
        /// into the BsonClassMap provided by the .NET MongoDB
        /// driver.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RegisterClass<T>()
        {
            BsonClassMap.RegisterClassMap<T>();
        }

        /// <summary>
        /// This is a way to check if a class has already been
        /// registered with the BsonClassMap.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsClassRegistered(Type type)
        {
            return BsonClassMap.IsClassMapRegistered(type);
        }

        /// <summary>
        /// Override this method to add any indexes you may want for your database
        /// </summary>
        protected virtual void EnsureIndexes()
        {
        }

		/// <summary>
		/// Override this method to add any class mappings you may want for your database
		/// </summary>
		protected virtual void SetUpClassMappings()
		{
		}
    }
}
