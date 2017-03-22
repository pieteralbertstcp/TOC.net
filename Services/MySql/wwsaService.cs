using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Repository.MySql;
using System.Linq.Dynamic;
using System.Data;

namespace Services.MySql
{
    public class wwsaService<T> : WWSADbContext where T : class 
    {
     #region Ctor
        public wwsaService() { }
        public wwsaService(WWSADbContext dbContext) : base(dbContext) { }
        #endregion


        #region Public Methods

        public T GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                return _dbContext.Set<T>().Where(predicate).SingleOrDefault();
            }

            throw new ArgumentNullException("Predicate value must be passed to FindSingleBy<T>.");
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                return _dbContext.Set<T>().Where(predicate).AsQueryable<T>(); ;
            }

            throw new ArgumentNullException("Predicate value must be passed to FindBy<T,TKey>.");
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate, int recordCount)
        {
            if (predicate != null)
            {
                return _dbContext.Set<T>().Where(predicate).AsQueryable<T>().Take(recordCount);
            }

            throw new ArgumentNullException("Predicate value must be passed to FindBy<T,TKey>.");
        }
        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll(int recordCount, int skip =0, Expression<Func<T, string>> predicate =null)
        {
            if (predicate == null)
                return _dbContext.Set<T>().Take(recordCount);
            else
                return _dbContext.Set<T>().Take(recordCount).OrderByDescending(predicate).Skip(skip);
        }
        public IEnumerable<T> Query(string sql, Dictionary<string, string> paramkeyvalues)
        {
            List<object> parameters = new List<object>();
            if (paramkeyvalues != null)
            {
                foreach (KeyValuePair<string, string> kv in paramkeyvalues)
                {
                    parameters.Add(QueryParameters.CreateMysqlQueryParams(kv.Key, kv.Value));
                }
            }
            if(parameters.Count == 0)
                return _dbContext.Database.SqlQuery<T>(sql, QueryParameters.CreateBlankMySqlParams());
            else
             return _dbContext.Database.SqlQuery<T>(sql, parameters.ToArray());
            
        }
        public IEnumerable<T> Query(string sql, Dictionary<string, string> paramkeyvalues, int recordCount)
        {
            List<object> parameters = new List<object>();
            if (paramkeyvalues != null)
            {
                foreach (KeyValuePair<string, string> kv in paramkeyvalues)
                {
                    parameters.Add(QueryParameters.CreateMysqlQueryParams(kv.Key, kv.Value));
                }
            }
            if (parameters.Count == 0)
                return _dbContext.Database.SqlQuery<T>(sql, QueryParameters.CreateBlankMySqlParams()).Take(recordCount);
            else
                return _dbContext.Database.SqlQuery<T>(sql, parameters.ToArray()).Take(recordCount);

        }
        public IEnumerable<T> ObjectQuery(string sql, Dictionary<string, string> paramkeyvalues)
        {
            List<object> parameters = new List<object>();
            if (paramkeyvalues != null)
            {
                foreach (KeyValuePair<string, string> kv in paramkeyvalues)
                {
                    parameters.Add(QueryParameters.CreateMysqlQueryParams(kv.Key, kv.Value));
                }
            }

            if (parameters.Count == 0)
                return _dbContext.Database.SqlQuery<T>(sql, QueryParameters.CreateBlankMySqlParams());
            else
                return _dbContext.Database.SqlQuery<T>(sql, parameters.ToArray());
        }
        public IEnumerable<T> ObjectQuery(string sql, Dictionary<string, string> paramkeyvalues, int recordCount)
        {
            List<object> parameters = new List<object>();
            if (paramkeyvalues != null)
            {
                foreach (KeyValuePair<string, string> kv in paramkeyvalues)
                {
                    parameters.Add(QueryParameters.CreateMysqlQueryParams(kv.Key, kv.Value));
                }
            }

            if (parameters.Count == 0)
                return _dbContext.Database.SqlQuery<T>(sql, QueryParameters.CreateBlankMySqlParams()).Take(recordCount);
            else
                return _dbContext.Database.SqlQuery<T>(sql, parameters.ToArray()).Take(recordCount);
        }

        public IQueryable<T> Musta(string queryValues)
        { 
            // values passed in would be ?where=ID:1234:GreaterThan
            var values = queryValues.Split(':');
            var wordToExpression = new Dictionary<string, string>
            {
                { "equal", "=="},
                { "notequal", "!="},
                { "greaterthan", ">" },
                { "greaterthanorequal", ">=" },
                { "lessthan", "<" },
                { "lessthanorequal", "<=" },
                { "like", "like" }
            };
            var right = values[1];
            Type propType = typeof(System.String);
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.Name == values[0])
                {
                    propType = prop.PropertyType;
                    break;
                }
                    
            }
            var searchValue = Convert.ChangeType(right, propType);

            // automatically assume equals operator
            if (values.Count() <= 2)
                return _dbContext.Set<T>().Where(string.Format("{0} == @0", values[0].ToString()), searchValue);
            else
            {
                // determine search type
                switch (wordToExpression[values[2]])
                {
                    // like compare
                    case "like":
                        return _dbContext.Set<T>().Where(string.Format("{0}.Contains(@0)", values[0].ToString(), wordToExpression[values[2]]), searchValue); 

                    // other compare
                    default:
                        return _dbContext.Set<T>().Where(string.Format("{0} {1} @0", values[0].ToString(), wordToExpression[values[2]]), searchValue);
                }
            }
        }

        public IQueryable<T> Musta(string queryValues, int recordCount)
        {
            // values passed in would be ?where=ID:1234:GreaterThan
            var values = queryValues.Split(':');
            var wordToExpression = new Dictionary<string, string>
            {
                { "equal", "=="},
                { "notequal", "!="},
                { "greaterthan", ">" },
                { "greaterthanorequal", ">=" },
                { "lessthan", "<" },
                { "lessthanorequal", "<=" },
            };
            var right = values[1];
            Type propType = typeof(System.String);
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.Name == values[0])
                {
                    propType = prop.PropertyType;
                    break;
                }

            }
            var serachValue = Convert.ChangeType(right, propType);

            if (values.Count() <= 2)
                return _dbContext.Set<T>().Where(string.Format("{0} == @0", values[0].ToString()), serachValue).Take(recordCount);
            else
                return _dbContext.Set<T>().Where(string.Format("{0} {1} @0", values[0].ToString(), wordToExpression[values[2]]), serachValue).Take(recordCount);
        }

        /// <summary>
        /// a more efficient way to do bulk insertion for multiple entities
        /// skips dataset checking and performs a save only once
        /// </summary>
        public int Create(IEnumerable<T> entities)
        {
            var autoDetectChangesEnabled = AutoDetectChangesEnabled;
            var validateOnSaveEnabled = ValidateOnSaveEnabled;
            var result = -1;

            // disable these flags to speed up insertion
            AutoDetectChangesEnabled = false;
            ValidateOnSaveEnabled = false;

            // add all entities
            foreach (var entity in entities)
                _dbContext.Set<T>().Add(entity);

            try
            {
                // save entities once only
                result = _dbContext.SaveChanges();
            }
            catch (OptimisticConcurrencyException e)
            {
                // we caught an optimistic concurrency exception
                // which happens when the database changes before we can save
                // in this case, we overwrite the database version with our data
                Refresh(RefreshMode.ClientWins, _dbContext.Set<T>());
                _dbContext.SaveChanges();
            }
            finally
            {
                // restore original states
                AutoDetectChangesEnabled = autoDetectChangesEnabled;
                ValidateOnSaveEnabled = validateOnSaveEnabled;
            }

            return result;
        }

        public T Create(T entity)
        {            
            var savedEntity = _dbContext.Set<T>().Add(entity);
            var id = _dbContext.SaveChanges();
            if (id != 1)
                return null;
            return savedEntity;
        }
        public T Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            
            var updateReturn = _dbContext.SaveChanges();
                if(updateReturn != 1)
                    return null;
            return entity;
        }

        public bool BulkUpdate(List<T> entities)
        {
            int updateReturn = 0;

            _dbContext.Entry<List<T>>(entities).State = System.Data.Entity.EntityState.Modified;
            
            updateReturn = _dbContext.SaveChanges();
            return updateReturn == 1;
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(List<T> entities)
        {
            bool autoDetectChangesEnabled = AutoDetectChangesEnabled;
            bool validateOnSaveEnabled = ValidateOnSaveEnabled;

            AutoDetectChangesEnabled = false;
            ValidateOnSaveEnabled = false;

            var set = _dbContext.Set<T>();

            // this should really be replaced with RemoveRange() in EF6 !
            // it's very costly, performance wise, to delete this way
            // alternate implementation is to directly execute SQL
            foreach (var entity in entities)
            {
                set.Remove(entity);
            }

            int result;

            try
            {
                result = _dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                result = -1; 
            }
            finally
            {
                // restore original states
                AutoDetectChangesEnabled = autoDetectChangesEnabled;
                ValidateOnSaveEnabled = validateOnSaveEnabled;
            }
            

            return result;
            
        }
        #endregion


    }
}
