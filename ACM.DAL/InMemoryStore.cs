using System;
using System.Collections.Generic;

namespace CMS.DataAccess
{
    /// <summary>
    /// Shares identifier allocation and copy isolation between in-memory repositories.
    /// </summary>
    internal sealed class InMemoryStore<TEntity> where TEntity : class
    {
        private readonly Dictionary<int, TEntity> _records = new Dictionary<int, TEntity>();
        private readonly Func<TEntity, int> _readId;
        private readonly Func<TEntity, int, TEntity> _cloneWithId;
        private int _nextKey = 1;

        public InMemoryStore(Func<TEntity, int> getId, Func<TEntity, int, TEntity> copyWithId)
        {
            if (getId == null) throw new ArgumentNullException("getId");
            if (copyWithId == null) throw new ArgumentNullException("copyWithId");

            _readId = getId;
            _cloneWithId = copyWithId;
        }

        public TEntity GetById(int id)
        {
            TEntity item;
            return _records.TryGetValue(id, out item) ? _cloneWithId(item, id) : null;
        }

        public IList<TEntity> GetAll()
        {
            var items = new List<TEntity>();
            foreach (var pair in _records)
            {
                items.Add(_cloneWithId(pair.Value, pair.Key));
            }
            return items;
        }

        public int Save(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");

            var storedId = _readId(item);
            var key = storedId > 0 ? storedId : _nextKey;
            _records[key] = _cloneWithId(item, key);
            if (key >= _nextKey) _nextKey = key + 1;
            return key;
        }
    }
}
