using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;

namespace Yetibyte.FridgeMvvm.DataExchange {

    public class DatabaseAccessor {

        #region Constants

        private const string ERROR_MSG_CONNECTION_STRING_BLANK = "The connection string must not be null or empty.";

        #endregion

        #region Static Properties

        #endregion

        #region Fields

        private string _connectionString;

        #endregion

        #region Properties

        public string ConnectionString {
            get => _connectionString;
            set => _connectionString = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentNullException(nameof(value), ERROR_MSG_CONNECTION_STRING_BLANK);
        }

        #endregion

        #region Constructors

        public DatabaseAccessor(string connectionString) {

            ConnectionString = connectionString;

        }

        #endregion

        #region Methods

        public ActionQueryResult ExecuteNonQuery(string sql, IEnumerable<SqlParameter> sqlParameters = null) {
            
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentNullException(nameof(sql));

            int numRowsAffected = 0;

            string errorMessage = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                
                SqlCommand sqlCommand = new SqlCommand(sql, connection);

                if (sqlParameters != null && sqlParameters.Any())
                    sqlCommand.Parameters.AddRange(sqlParameters.ToArray());

                try {

                    connection.Open();

                    numRowsAffected = sqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex) {

                    errorMessage = ex.Message;

                }

            }

            return new ActionQueryResult(numRowsAffected, errorMessage);

        }

        

        public FetchQueryResult<T> Fetch<T>(string sql, Func<DbDataReader, T> objectInitializer, IEnumerable<SqlParameter> sqlParameters = null) {

            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentNullException(nameof(sql));

            if (objectInitializer == null)
                throw new ArgumentNullException(nameof(objectInitializer));

            List<T> results = new List<T>();

            string errorMessage = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString)) {

                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                
                if (sqlParameters != null && sqlParameters.Any())
                    sqlCommand.Parameters.AddRange(sqlParameters.ToArray());

                try {

                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                        results.Add(objectInitializer(reader));


                }
                catch(Exception ex) {

                    errorMessage = ex.Message;
                    
                }

            }

            return new FetchQueryResult<T>(results, errorMessage);

        }

        public virtual bool TestConnection() {

            bool success = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString)) {

                try {
                    connection.Open();

                    success = connection.State == System.Data.ConnectionState.Open;

                }
                catch {
                    
                }

            }

            return success;

        }

        #endregion

    }

}
