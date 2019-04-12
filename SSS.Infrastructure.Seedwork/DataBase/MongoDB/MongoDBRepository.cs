using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SSS.Domain.Seedwork.Model;
using SSS.Domain.Seedwork.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SSS.Infrastructure.Seedwork.DataBase.MongoDB
{
    public class MongoDBRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

        private readonly MongoClient _client;

        private readonly ILogger _logger;

        private readonly IMongoDatabase _db;

        protected readonly IMongoCollection<TEntity> _collection;

        public MongoDBRepository(IOptions<MongoOptions> options, ILogger<MongoDBRepository<TEntity>> logger)
        {
            _logger = logger;

            try
            {
                if (!string.IsNullOrWhiteSpace(options.Value.host))
                    _client = new MongoClient("mongodb://" + options.Value.host + ":" + options.Value.port);

                else
                    _client = new MongoClient("mongodb://localhost:27017");

                _db = _client.GetDatabase(typeof(TEntity).Name);

                _collection = _db.GetCollection<TEntity>(typeof(TEntity).Name);

            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, ex.Message);
            }
        }

        public void Add(TEntity obj)
        {
            _collection.InsertOne(obj);
        }

        public TEntity Get(Guid id)
        {
            return _collection.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _collection.AsQueryable();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).ToList().AsQueryable();
        }

        public IQueryable<TEntity> GetPage(int index, int size, ref int count)
        {
            count = _collection.AsQueryable().Count();
            return _collection.AsQueryable().Skip(size * (index > 0 ? index - 1 : 0)).Take(size);
        }

        public IQueryable<TEntity> GetPage(int index, int size, Expression<Func<TEntity, bool>> predicate, ref int count)
        {
            count = _collection.AsQueryable().Count();
            return _collection.AsQueryable().Where(predicate).Skip(size * (index > 0 ? index - 1 : 0)).Take(size);
        }

        public void Update(TEntity obj)
        {
            _collection.ReplaceOne(x => x.Id.Equals(obj.Id), obj);
        }

        public void Remove(Guid id)
        {
            _collection.DeleteMany(x => x.Id.Equals(id));
        }

        public int SaveChanges() => throw new NotImplementedException();

        public void Dispose() => throw new NotImplementedException();
    }
}
