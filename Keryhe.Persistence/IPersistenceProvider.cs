using System.Collections.Generic;
using System.Data;

namespace Keryhe.Persistence
{
    public interface IPersistenceProvider
    {
        IDataReader ExecuteQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters);
        IDataReader ExecuteQuery(string commandText, CommandType commandType);
        IDataReader ExecuteQuery(string commandText);

        int ExecuteNonQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters, ref Dictionary<string, object> outputParameters);
        int ExecuteNonQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters);
        int ExecuteNonQuery(string commandText, CommandType commandType);
        int ExecuteNonQuery(string commandText);

        IDbTransaction BeginTransaction();
        void CommitTransaction(IDbTransaction transaction);
        void RollbackTransaction(IDbTransaction transaction);

        IDataReader ExecuteQuery(IDbTransaction transaction, string commandText, CommandType commandType, Dictionary<string, object> parameters);
        IDataReader ExecuteQuery(IDbTransaction transaction, string commandText, CommandType commandType);
        IDataReader ExecuteQuery(IDbTransaction transaction, string commandText);

        int ExecuteNonQuery(IDbTransaction transaction, string commandText, CommandType commandType, Dictionary<string, object> parameters);
        int ExecuteNonQuery(IDbTransaction transaction, string commandText, CommandType commandType);
    }
}
