using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoxBraydonProject4
{
    public class QueryController
    {
        private SqlConnection databaseConnection;
        private SqlCommand databaseCommand;

        public QueryController()
        {
            databaseConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Courses"].ConnectionString);
        }

        public void connectToDatabase()
        {
            databaseConnection.Open();
        }

        public void disconnectFromDatabase()
        {
            databaseConnection.Close();
        }

        public void createDatabaseCommand(string databaseQuery)
        {
            databaseCommand = new SqlCommand(databaseQuery, databaseConnection);
        }

        public SqlCommand getDatabaseCommand()
        {
            return databaseCommand;
        }

        public void addQueryParameter(string parameterName, object parameterValue)
        {
            databaseCommand.Parameters.AddWithValue(parameterName, parameterValue);
        }

        public DataTable runSelectQuery()
        {
            DataTable queryResults = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(databaseCommand);
            dataAdapter.Fill(queryResults);
            return queryResults;
        }
    }
}