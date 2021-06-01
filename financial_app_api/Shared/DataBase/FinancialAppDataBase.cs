using System;
using System.Data.SqlClient;

namespace financial_app_api.Shared.DataBase
{
    public class FinancialAppDataBase : IDisposable
    {
        private SqlConnection _cnn = null;
        private SqlTransaction _transaction = null;

        private void Setup()
        {
            if (_cnn == null)
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
                sb.DataSource = "financialdb.cn42zhgmyc6o.us-east-2.rds.amazonaws.com,2435";
                sb.UserID = "masteruser";
                sb.Password = "financialdb123456";
                sb.InitialCatalog = "financialdb"; //confirmar
                sb.ConnectTimeout = 10;
                _cnn = new SqlConnection(sb.ToString());
                _cnn.Open();
            }
        }
        public SqlCommand GetCmd()
        {
            return GetCmd(null);
        }
        public SqlCommand GetCmd(string select)
        {
            Setup();
            var cmd = _cnn.CreateCommand();
            cmd.CommandTimeout = 10;
            cmd.Transaction = _transaction;
            cmd.CommandText = select;
            return cmd;
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }
        public void Begin()
        {
            if (_transaction != null)
                throw new Exception("There's already an open transaction");
            Setup();
            _transaction = _cnn.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

      
        public void Dispose()
        {
            try
            {
                try
                {
                    Rollback();
                }
                catch { }

                using (_cnn)
                {

                }
            }
            catch { }
            finally
            {
                _transaction = null;
                _cnn = null;
            }
        }
        protected virtual void Dispose(bool native)
        {
            this.Dispose();
        }
    }
}

