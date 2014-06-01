using System;
using MongoDB.Driver;

namespace MongoCrudNet.Mongo
{
    /// <summary>
    /// This interface defines what is needed to provide MongoDB access to a
    /// CRUD repository based on MongoDB.
    /// </summary>
    public interface IMongoDbContext
    {
        MongoServer Server { get; }
        MongoDatabase Database { get; }
        bool Initialized { get; }

        /// <summary>
        /// Connects to the server and the database
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        bool Connect(string connectionString, string databaseName);

        /// <summary>
        /// Registers a class for serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void RegisterClass<T>();

        /// <summary>
        /// Checks to see if a class is already registered with serialization
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsClassRegistered(Type type);
    }
}
