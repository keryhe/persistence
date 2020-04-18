using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;
using Keryhe.Persistence;

namespace Keryhe.Persistence.Tests
{
    public class PersistenceExtensionsTest
    {
        [Fact]
        public void ExecuteNonQueryTest()
        {
            Mock<IDbDataParameter> parameter = new Mock<IDbDataParameter>();

            Mock<IDbCommand> cmd = new Mock<IDbCommand>();
            cmd.Setup(x => x.CreateParameter()).Returns(parameter.Object);
            cmd.Setup(x => x.ExecuteNonQuery()).Returns(1);

            Mock<IDbConnection> conn = new Mock<IDbConnection>();
            conn.Setup(x => x.CreateCommand()).Returns(cmd.Object);

            IDbConnection connection = conn.Object;
            var result = connection.ExecuteNonQuery("INSERT INTO test");

            Assert.Equal(1, result);

        }

        [Fact]
        public void ExecuteQueryTest()
        {
            Mock<IDbDataParameter> parameter = new Mock<IDbDataParameter>();

            Mock<IDataReader> reader = new Mock<IDataReader>();
            reader.Setup(x => x.FieldCount).Returns(4);
            reader.Setup(x => x.GetName(0)).Returns("Id");
            reader.Setup(x => x.GetName(1)).Returns("Name");
            reader.Setup(x => x.GetName(2)).Returns("Description");
            reader.Setup(x => x.GetName(3)).Returns("Status");
            reader.Setup(x => x["Id"]).Returns(1234);
            reader.Setup(x => x["Name"]).Returns("test");
            reader.Setup(x => x["Description"]).Returns(DBNull.Value);
            reader.Setup(x => x["Status"]).Returns(true);

            reader.SetupSequence(x => x.Read()).Returns(true).Returns(false);

            Mock<IDbCommand> cmd = new Mock<IDbCommand>();
            cmd.Setup(x => x.CreateParameter()).Returns(parameter.Object);
            cmd.Setup(x => x.ExecuteReader()).Returns(reader.Object);

            Mock<IDbConnection> conn = new Mock<IDbConnection>();
            conn.Setup(x => x.CreateCommand()).Returns(cmd.Object);

            IDbConnection connection = conn.Object;
            var result = connection.ExecuteQuery("SELECT * FROM test");

            Assert.Single(result);
            Assert.Equal(1234, result[0]["Id"]);
            Assert.Equal("test", result[0]["Name"]);
            Assert.Null(result[0]["Description"]);
            Assert.Equal(true, result[0]["Status"]);
        }
    }
}
