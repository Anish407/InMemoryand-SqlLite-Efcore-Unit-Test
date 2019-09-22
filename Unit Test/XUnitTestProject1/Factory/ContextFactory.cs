using DAL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject1.Factory
{
    public class ContextFactory : IDisposable
    {

        #region IDisposable Support  
        private bool disposedValue = false; // To detect redundant calls 
        DbContextOptions<SchoolContext> inMemoryOption { get; set; }
        DbContextOptions<SchoolContext> SqlIteoption { get; set; }
        public ContextFactory()
        {
            inMemoryOption = new DbContextOptionsBuilder<SchoolContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options;
            SqlIteoption = GetSqlLiteOptions();
        }

        public SchoolContext CreateContextForInMemory()
        {
            

            var context = new SchoolContext(inMemoryOption);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public SchoolContext CreateContextForSQLite()
        {
            

            var context = new SchoolContext(SqlIteoption);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        private static DbContextOptions<SchoolContext> GetSqlLiteOptions()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            DbContextOptions<SchoolContext> option = new DbContextOptionsBuilder<SchoolContext>().UseSqlite(connection).Options;
            return option;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
