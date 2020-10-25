# Cloud.Framework.Persistence.Abstractions

## Description
The base framework for accessing a **SQL** database. The primary usage is for [Dapper](https://dapper-tutorial.net/dapper).

### Important Classes
- [DbQuery](./Sql.Base/DbQuery.cs): This struct is used to pass around SQL-related information such as the query and parameters. The best thing to do is create an extension to create a **CommandDefinition** in Dapper.
- [IDbSettings](./Sql.Interfaces/IDbSettings.cs): This interface is used to abstract the type of connection that will be used as well as the underlying creation of the connection.
- [IDbContext](./Sql.Interfaces/IDbContext.cs): This interface is used to specify the type of connection context as well as opening a connection and creating transactions.