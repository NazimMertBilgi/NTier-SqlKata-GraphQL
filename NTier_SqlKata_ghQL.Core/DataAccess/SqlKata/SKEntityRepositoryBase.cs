﻿using NTier_SqlKata_ghQL.Core.Entities;
using SqlKata;
using SqlKata.Execution;

namespace NTier_SqlKata_ghQL.Core.DataAccess.SqlKata
{
    public class SKEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {

        protected readonly QueryFactory _db;
        protected readonly XQuery _dbXQuery;

        public SKEntityRepositoryBase(QueryFactory db, XQuery dbXQuery)
        {
            _db = db;
            _dbXQuery = dbXQuery;
        }

        public IEnumerable<dynamic> Sql(string sql, dynamic? parameters = null)
        {
            return _db.Select(sql, parameters);
        }

        public IEnumerable<dynamic> ExecQuery(Query query)
        {
            return _db.FromQuery(query).Get();
        }

        public async Task<IEnumerable<dynamic>> ExecQueryAsync(Query query)
        {
            return await _db.FromQuery(query).GetAsync();
        }

        public Query ExecQueryWithoutGet(Query query)
        {
            return _db.FromQuery(query);
        }

        public XQuery XQuery()
        {
            return _dbXQuery;
        }

        public IEnumerable<dynamic> Add(Query query, TEntity entity)
        {
            return _db.FromQuery(query).AsInsert(entity).Get();
        }

        public IEnumerable<dynamic> Update(Query query, object entity)
        {
            return _db.FromQuery(query).AsUpdate(entity).Get();
        }

        public IEnumerable<dynamic> Delete(Query query)
        {
            return _db.FromQuery(query).AsDelete().Get();
        }

    }
}
