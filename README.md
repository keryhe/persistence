# Keryhe.Persistence

![Keryhe.Persistence](https://img.shields.io/nuget/v/Keryhe.Persistence.svg)

Adds ExecuteQuery and ExecuteNonQuery extension methods to IDbConnection. Results are returned as a Dictionary<string, object> list, where the key is the column name and the value is the column value.

## Methods

```c#
public static int ExecuteNonQuery(this IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters, Dictionary<string, object> outputParameters)

public static int ExecuteNonQuery(this IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters)

public static int ExecuteNonQuery(this IDbConnection connection, string commandText, CommandType commandType)

public static int ExecuteNonQuery(this IDbConnection connection, string commandText)

public static List<Dictionary<string, object>> ExecuteQuery(this IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters)

public static List<Dictionary<string, object>> ExecuteQuery(this IDbConnection connection, string commandText, CommandType commandType)

public static List<Dictionary<string, object>> ExecuteQuery(this IDbConnection connection, string commandText)
```

## Usage

```c#
using(SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    var results = connection.ExecuteQuery("select * from Test");
}
```
