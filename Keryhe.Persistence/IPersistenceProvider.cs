using System.Collections.Generic;
using System.Data;
using System.Transactions;

namespace Keryhe.Persistence
{
    public interface IPersistenceProvider
    {
        IDbConnection CreateConnection();

        IDataReader ExecuteQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters);
        IDataReader ExecuteQuery(IDbConnection connection, string commandText, CommandType commandType);
        IDataReader ExecuteQuery(IDbConnection connection, string commandText);

        int ExecuteNonQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters, Dictionary<string, object> outputParameters);
        int ExecuteNonQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters);
        int ExecuteNonQuery(IDbConnection connection, string commandText, CommandType commandType);
        int ExecuteNonQuery(IDbConnection connection, string commandText);
    }
}
