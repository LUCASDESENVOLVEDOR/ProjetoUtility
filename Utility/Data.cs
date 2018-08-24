using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Utility
{
    public abstract class Data
    {
        private static string connectionString = string.Empty;

        static Data()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            }

            if (connectionString == string.Empty)
            {

                if (ConfigurationManager.AppSettings["default"] == null)
                {
                    connectionString = ConfigurationManager.AppSettings["default"].ToString();
                }
            }

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Não foi encontrado uma conexão.");
        }

        public static int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, null, CommandType.StoredProcedure);
        }

        public static int ExecuteNonQuery(string commandText, List<SqlParameter> parameters)
        {
            return ExecuteNonQuery(commandText, parameters, CommandType.StoredProcedure);
        }

        public static int ExecuteNonQuery(string commandText, List<SqlParameter> parameters, CommandType commandType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = commandType;

                    if (parameters != null)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    connection.Open();
                    int recordsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                    return recordsAffected;
                }
            }
        }


        public static object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, null, CommandType.StoredProcedure);
        }

        public static object ExecuteScalar(string commandText, List<SqlParameter> parameters)
        {
            return ExecuteScalar(commandText, parameters, CommandType.StoredProcedure);
        }

        public static object ExecuteScalar(string commandText, List<SqlParameter> parameters, CommandType commandType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = commandType;

                    if (parameters != null)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    connection.Open();
                    object unknownType = command.ExecuteScalar();
                    connection.Close();
                    connection.Dispose();
                    return unknownType;
                }
            }
        }

        public static DataTable ExecuteDataTable(string commandText)
        {

            return ExecuteDataTable(commandText, null, CommandType.StoredProcedure);
        }

        public static DataTable ExecuteDataTable(string commandText, List<SqlParameter> parameters)
        {
            return ExecuteDataTable(commandText, parameters, CommandType.StoredProcedure);
        }

        public static DataTable ExecuteDataTable(string commandText, List<SqlParameter> parameters, CommandType commandType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = commandType;

                    if (parameters != null)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    connection.Open();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(command.ExecuteReader());
                    connection.Close();

                    connection.Dispose();

                    return dataTable;
                }
            }
        }

        public static DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(commandText, null, CommandType.StoredProcedure);
        }

        public static DataSet ExecuteDataSet(string commandText, List<SqlParameter> parameters)
        {
            return ExecuteDataSet(commandText, parameters, CommandType.StoredProcedure);
        }

        public static DataSet ExecuteDataSet(string commandText, List<SqlParameter> parameters, CommandType commandType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = commandType;

                    if (parameters != null)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddRange(parameters.ToArray());
                    }
                    command.CommandTimeout = 600;
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    connection.Close();
                    connection.Dispose();
                    return dataSet;
                }
            }
        }

        public static SqlParameter CreateParameter(string name, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.SqlValue = value;
            parameter.Value = value;
            if (value == null)
            {
                parameter.SqlValue = DBNull.Value;
                parameter.IsNullable = true;
            }
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

    }
}
