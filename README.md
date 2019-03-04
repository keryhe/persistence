# persistence

A persistence wrapper for interacting with relational databases. It currently supports the following databases:

- MySql
- PostgreSQL
- Microsoft Sql Server

# Keryhe.Persistence

![Keryhe.Persistence](https://img.shields.io/nuget/v/Keryhe.Persistence.svg)

To implement the IPersistent interface for a particular database, install the [Keryhe.Persistence](https://www.nuget.org/packages/keryhe.persistence) package from NuGet.

Use ExecuteQuery to implement select statements or call stored procedures that return data.

```c#
IDataReader ExecuteQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters);
        IDataReader ExecuteQuery(string commandText, CommandType commandType);
        IDataReader ExecuteQuery(string commandText);
```

Use ExecuteNonQuery to implement Insert, Update, or Delete statements as well as stored procedures that insert update or delete data.

```c#
int ExecuteNonQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters, ref Dictionary<string, object> outputParameters);
        int ExecuteNonQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters);
        int ExecuteNonQuery(string commandText, CommandType commandType);
        int ExecuteNonQuery(string commandText);
```

# Keryhe.Persistence.MySql

![Keryhe.Persistence.MySql](https://img.shields.io/nuget/v/Keryhe.Persistence.MySql.svg)

A MySql implementation of the IPersistence interface. Uses the [MySql.Data](https://www.nuget.org/packages/mysql.data) package.

Install the [Keryhe.Persistence.MySql](https://www.nuget.org/packages/keryhe.persistence.mysql) package from NuGet to use MySql as your database.

# Keryhe.Persistence.PostgreSQL

![Keryhe.Persistence.PostgresSql](https://img.shields.io/nuget/v/Keryhe.Persistence.PostgreSQL.svg)

A PostgreSQL implementation of the IPersistence interface. Uses the [Npgsql](https://www.nuget.org/packages/npgsql) package.

Install the [Keryhe.Persistence.PostgreSQL](https://www.nuget.org/packages/keryhe.persistence.postgresql) package from NuGet to use PostgreSQL as your database.

# Keryhe.Persistence.SqlServer

![Keryhe.Persistence.SqlServer](https://img.shields.io/nuget/v/Keryhe.Persistence.SqlServer.svg)

A Sql Server implementation of the IPersistence interface. Uses the [System.Data.SqlClient](https://www.nuget.org/packages/system.data.sqlclient) package.

Install the [Keryhe.Persistence.SqlServer](https://www.nuget.org/packages/keryhe.persistence.sqlserver) package from NuGet to use Microsoft Sql Server as your database.