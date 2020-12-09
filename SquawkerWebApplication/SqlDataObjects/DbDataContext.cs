using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Collections.Generic;

namespace SquawkerWebApplication.SqlDataObjects
{
    [Database(Name = "aspnet-Test_WebApplication001-20201203041347")]
    public partial class DbDataContext : DataContext
    {
        private static MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        partial void InsertSquawks(Squawks instance);
        partial void UpdateSquawks(Squawks instance);
        partial void DeleteSquawks(Squawks instance);
        partial void InsertUsers(Users instance);
        partial void UpdateUsers(Users instance);
        partial void DeleteUsers(Users instance);
        #endregion

        public DbDataContext(string connection) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public DbDataContext(IDbConnection connection) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public DbDataContext(string connection, MappingSource mappingSource) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public DbDataContext(IDbConnection connection, MappingSource mappingSource) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public Table<Squawks> Squawks
        {
            get
            {
                return this.GetTable<Squawks>();
            }
        }

        public Table<Users> Users
        {
            get
            {
                return this.GetTable<Users>();
            }
        }
    }
}
