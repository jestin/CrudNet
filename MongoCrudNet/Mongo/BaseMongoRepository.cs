using System;
using System.Collections.Generic;
using MongoCrudNet.CRUD;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoCrudNet.Mongo
{
    /// <summary>
    /// This is a base CRUD repository for MongoDB.  Due to the extremely flexible nature of MongoDB,
    /// this base class implements all the functionality needed to create a repository, unless special
    /// data relationships or functionality is required.  All one has to do is create a repository that
    /// specifies a domain object template, specifies the CRUD interfaces it implements, and use this
    /// class as a base class.  The agile abilities of MongoDB will handle the rest, making database
    /// access simple and easy
    /// 
    /// To the constructor of your derived class, just add the following code, substituting values as
    /// you need:
    /// 
    /// ConnectionString = "mongodb://127.0.0.1/?safe=true";
    /// DatabaseName = "mongo_example";
    /// ConnectToDatabase();
    /// CollectionName = "Examples"
    /// 
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseMongoRepository<T> : ICreatable<T>, IRetrievable<T>, IUpdatable<T>, IDeletable<T>, IBulkRetrievable<T> where T : new()
    {
        #region Dependencies

        protected readonly IMongoDbContext Db;

        #endregion

        #region Member Variables

        private MongoCollection _collection;

        /// <summary>
        /// This must be set by a child class
        /// </summary>
        protected string ConnectionString;

        /// <summary>
        /// This must be set by a child class
        /// </summary>
        protected string DatabaseName;

        #endregion

        #region Properties

        /// <summary>
        /// This name must be specified in a child class's constructor.  It will determine
        /// the name of the collection this repository uses to store its documents
        /// </summary>
        protected string CollectionName { get; set; }

        /// <summary>
        /// This property provides direct access to the underlying MongoCollection, in case
        /// more specific functionality is needed by a child class
        /// </summary>
        protected virtual MongoCollection Collection
        {
            get
            {
                // check if the programmer remembered to tell MongoDB what collection to store this data in
                if(string.IsNullOrEmpty(CollectionName))
                {
                    // throw a more useful exception
                    throw new Exception("CollectionName not specified.  Did you forget to initialize CollectionName in your repository's constructor?");
                }

                return _collection ?? (_collection = Db.Database.GetCollection<T>(CollectionName));
            }
        }

        #endregion

        #region Constructors

        protected BaseMongoRepository(IMongoDbContext dbContext)
        {
            Db = dbContext;
        }

        /// <summary>
        /// This method must be called after the connection string and database name have been set, but before
        /// any other operations are called.  The best place is at the end of the constructor of the repository's
        /// implementation, or in the constructor of a derived abstract base class.
        /// </summary>
        protected void ConnectToDatabase()
        {
            if (Db.Initialized) return;

            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception("A connection string has not been specified.  Have you overridden BaseMongoRepository?");
            }

            if (string.IsNullOrEmpty(DatabaseName))
            {
                throw new Exception("A database name has not been specified.  Have you overridden BaseMongoRepository?");
            }

            try
            {
                Db.Connect(ConnectionString, DatabaseName);
            }
            catch (Exception)
            {
                throw new Exception("Could not connect to the mongo database");
            }
        }

        #endregion

        /// <summary>
        /// This method needs to be implemented by child classes, even if it
        /// doesn't do anything.  The intention is that it performs whatever
        /// tasks are necessary for initializing a domain object.
        /// </summary>
        /// <param name="item"></param>
        protected abstract void InitializeItem(ref T item);

        #region ICreatable Implementation

        /// <summary>
        /// This version of Create is used to call InitializeItem, which in
        /// turn can be used for a variety of initializations.  Although
        /// MongoDB doesn't require you to insert objects created by the
        /// database, this method should still be used to crate database
        /// objects in the case that you switch to a database that does
        /// require this.
        /// </summary>
        /// <returns></returns>
        public virtual T Create()
        {
            var item = new T();
            InitializeItem(ref item);
            return item;
        }

        /// <summary>
        /// This Create method is about as basic as it comes.  It inserts
        /// the object, and returns the object as though it has just
        /// recieved it from the database, although in truth it hasn't.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual T Create(T item)
        {
            Collection.Insert(item);
            return item;
        }

        #endregion

        #region IRetrievable Implementation

        /// <summary>
        /// This is the method that retrieves an object based on
        /// that object's key.  For now, it uses the Retrieve
        /// with multiple keys to do its work.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Retrieve(object key)
        {
            return Retrieve(new[] { key });
        }

        /// <summary>
        /// Although this Retrieve method takes multiple keys, it
        /// only looks at a single one.  For most cases, this is
        /// the correct behavior, but this should be overridden
        /// for repositories of objects that use complex keys.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual T Retrieve(object[] keys)
        {
            try
            {
                var id = ConvertToBsonObjectId(keys[0]);
                return Collection.FindOneAs<T>(Query.EQ("_id", id.ToString()));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        #endregion

        #region IUpdatable Implementation

        /// <summary>
        /// This implementation of Update is very simple.
        /// It updates the object, and then returns a the
        /// same object as if it was read from the
        /// database again.  Although this is not the case
        /// it is good practice for the user of this CRUD
        /// setup to use the object returned by Update
        /// rather than the one passed in.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual T Update(T item)
        {
            Collection.Save(item);
            return item;
        }

        #endregion

        #region IDeletable Implementation

        /// <summary>
        /// This Delete method deletes documents based on a
        /// complex key, however in this implementation only
        /// the first key is used.  This is how things will
        /// work in a typical case where there is only a
        /// single key for an object, and therefore this
        /// method will need to be overridden for a repository
        /// that uses complex keys.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual bool Delete(object[] keys)
        {
            try
            {
                var id = ConvertToBsonObjectId(keys[0]);
                var result = Collection.Remove(Query.EQ("_id", id.ToString()));
                return !result.HasLastErrorMessage;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// This delete method is impossible to write without knowing the specifics of the domain object used,
        /// however can be implemented in a derived class quite easily.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Delete(T item)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// This DeleteAll will remove all documents from the repository's
        /// MongoCollection.  It verifies this by checking if an
        /// error occurred.
        /// </summary>
        /// <returns></returns>
        public virtual bool DeleteAll()
        {
            var result = Collection.RemoveAll();
            return !result.HasLastErrorMessage;
        }

        /// <summary>
        /// This DeleteAll method will delete all objects that have a
        /// specific complex key.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual bool DeleteAll(object[] keys)
        {
            var id = ConvertToBsonObjectId(keys[0]);
            var result =  Collection.Remove(Query.EQ("_id", id.ToString()));
            return !result.HasLastErrorMessage;
        }

        #endregion

        #region IBulkRetrievable Implementation

        /// <summary>
        /// This retrieves all the documents in the repository's
        /// collection.  This method is often used in conjunction
        /// with Linq in order to implement a wide variety of
        /// queries.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> RetrieveAll()
        {
            return Collection.FindAllAs<T>();
        }

        /// <summary>
        /// This method retrieves all the documents with a given
        /// key.  In this implementation it has no advantage over
        /// using Retrieve.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> RetrieveAll(object key)
        {
            var id = ConvertToBsonObjectId(key);
            return Collection.FindAs<T>(Query.EQ("_id", id.ToString()));
        }

        /// <summary>
        /// This method retrieves all the documents with a given
        /// complex key.  In this implementation it has no advantage
        /// over using Retrieve, especially since only a single key
        /// is used.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> RetrieveAll(object[] keys)
        {
            var id = ConvertToBsonObjectId(keys[0]);
            return Collection.FindAs<T>(Query.EQ("_id", id.ToString()));
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// This method helps convert a regular 'object' into an Id
        /// that MongoDB can use.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual BsonObjectId ConvertToBsonObjectId(object obj)
        {
            try
            {
                return new BsonObjectId(obj is string ? new ObjectId((string)obj) : (ObjectId)obj);
            }
            catch (Exception)
            {
                return ObjectId.GenerateNewId();
            }
        }

        #endregion
    }
}
