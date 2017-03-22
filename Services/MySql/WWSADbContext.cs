using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.MySql;

namespace Services.MySql
{
    public class WWSADbContext : IDisposable
    {
        protected wwsaEntities _dbContext;    // this is protected for a reason, do not change it
        private bool disposed = false;


        #region Ctor
        protected WWSADbContext()
        {
            _dbContext = new wwsaEntities();
            var objectContext = (_dbContext as IObjectContextAdapter).ObjectContext;
        }

        protected WWSADbContext(WWSADbContext dbContext)
        {
            _dbContext = dbContext._dbContext;
        }
        #endregion

        #region Destructor
        ~WWSADbContext()
        {
            Dispose(true);
        }
        #endregion

        #region Properties
        public bool AutoDetectChangesEnabled
        {
            get { return _dbContext.Configuration.AutoDetectChangesEnabled; }
            set { _dbContext.Configuration.AutoDetectChangesEnabled = value; }
        }

        public bool LazyLoadingEnabled
        {
            get { return _dbContext.Configuration.LazyLoadingEnabled; }
            set { _dbContext.Configuration.LazyLoadingEnabled = value; }
        }

        public bool ProxyCreationEnabled
        {
            get { return _dbContext.Configuration.ProxyCreationEnabled; }
            set { _dbContext.Configuration.ProxyCreationEnabled = value; }
        }

        public bool ValidateOnSaveEnabled
        {
            get { return _dbContext.Configuration.ValidateOnSaveEnabled; }
            set { _dbContext.Configuration.ValidateOnSaveEnabled = value; }
        }

        #endregion

        #region Public Methods
        // public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// for OptimisticConcurrencyException exceptions
        /// used when we want to force client changes through even though
        /// db data has changed, usually
        /// </summary>
        public void Refresh(RefreshMode mode, IEnumerable collection)
        {
            var objectContext = ((IObjectContextAdapter)_dbContext).ObjectContext;
            objectContext.Refresh(mode, collection);
        }

        /// <summary>
        /// executes SQL and brings back returned objects
        /// in a dynamic, dictionary list
        /// </summary>
        public List<dynamic> ExecuteSelect(string selectSql)
        {
            var results = new List<dynamic>();

            // if no SQL to execute, return an empty set
            if (string.IsNullOrWhiteSpace(selectSql)) return results;

            // open new database connection
            _dbContext.Database.Connection.Open();

            using (var cmd = _dbContext.Database.Connection.CreateCommand())
            {
                cmd.CommandText = selectSql;
                // use the command timeout value set within
              
                using (var reader = cmd.ExecuteReader())
                {
                    var fields = new string[reader.FieldCount < 1 ? 1 : reader.FieldCount];

                    // collect the different field names, being sure to
                    // strip out any spaces
                    var x = 0;
                    for (; x < reader.FieldCount; x++)
                        fields[x] = reader.GetName(x).Replace(" ", "");

                    // read in each row received
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>(reader.FieldCount + 1);

                        for (x = 0; x < reader.FieldCount; x++)
                            row.Add(fields[x], reader[x]);

                        results.Add(row);
                    }
                }
            }

            // close database connection
            if (_dbContext.Database.Connection.State == System.Data.ConnectionState.Open)
                _dbContext.Database.Connection.Close();

            return results;
        }
        #endregion

        #region Protected Methods
        // protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                if (_dbContext != null)
                {
                    // free any other managed objects here...
                    _dbContext.Dispose();
                }
            }

            // free any unmanaged objects here...
            disposed = true;
        }
        #endregion
    }
}
