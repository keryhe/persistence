using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Keryhe.Persistence
{
    public static class PersistenceExtensions
    {

        public static int ExecuteNonQuery(this IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters, Dictionary<string, object> outputParameters)
        {
            IDbCommand cmd = connection.CreateCommand();

            cmd.CommandType = commandType;
            cmd.CommandText = commandText;

            if (parameters != null)
            {
                foreach (string key in parameters.Keys)
                {
                    IDbDataParameter parameter = cmd.CreateParameter();
                    if (parameters[key] == null)
                    {
                        parameter.ParameterName = key;
                        parameter.Value = DBNull.Value;
                        parameter.Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        parameter.ParameterName = key;
                        parameter.Value = parameters[key];
                        parameter.Direction = ParameterDirection.Input;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }

            if (outputParameters != null)
            {
                foreach (string key in outputParameters.Keys)
                {
                    IDbDataParameter outputParameter = cmd.CreateParameter();
                    outputParameter.ParameterName = key;
                    outputParameter.Value = parameters[key];
                    outputParameter.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(outputParameter);
                }
            }

            int result = cmd.ExecuteNonQuery();

            if (outputParameters != null)
            {
                foreach (IDbDataParameter parameter in cmd.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        object value = parameter.Value;
                        if (value == DBNull.Value)
                        {
                            value = null;
                        }
                        outputParameters.Add(parameter.ParameterName, value);
                    }
                }
            }

            return result;
        }

        public static int ExecuteNonQuery(this IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            return ExecuteNonQuery(connection, commandText, commandType, parameters, null);
        }

        public static int ExecuteNonQuery(this IDbConnection connection, string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(connection, commandText, commandType, null);
        }

        public static int ExecuteNonQuery(this IDbConnection connection, string commandText)
        {
            return ExecuteNonQuery(connection, commandText, CommandType.Text);
        }

        public static List<Dictionary<string, object>> ExecuteQuery(this IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            IDbCommand cmd = connection.CreateCommand();

            cmd.CommandType = commandType;
            cmd.CommandText = commandText;

            if (parameters != null)
            {
                foreach (string key in parameters.Keys)
                {
                    IDbDataParameter parameter = cmd.CreateParameter();
                    if (parameters[key] == null)
                    {
                        parameter.ParameterName = key;
                        parameter.Value = DBNull.Value;
                        parameter.Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        parameter.ParameterName = key;
                        parameter.Value = parameters[key];
                        parameter.Direction = ParameterDirection.Input;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }

            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();

                    var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                    foreach (var column in columns)
                    {
                        object columnValue = reader[column];
                        if (columnValue == DBNull.Value)
                        {
                            result.Add(column, null);
                        }
                        else
                        {
                            result.Add(column, columnValue);
                        }
                    }
                    results.Add(result);
                }
            }

            return results;
        }

        public static List<Dictionary<string, object>> ExecuteQuery(this IDbConnection connection, string commandText, CommandType commandType)
        {
            return ExecuteQuery(connection, commandText, commandType, null);
        }

        public static List<Dictionary<string, object>> ExecuteQuery(this IDbConnection connection, string commandText)
        {
            return ExecuteQuery(connection, commandText, CommandType.Text, null);
        }
    }
}
