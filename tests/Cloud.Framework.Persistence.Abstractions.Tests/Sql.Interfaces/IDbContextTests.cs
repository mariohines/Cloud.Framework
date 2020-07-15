using System.Data.Common;
using Cloud.Framework.Persistence.Abstractions.Sql.Interfaces;
using FluentAssertions;
using MySqlConnector;
using NSubstitute;
using Xunit;

namespace Cloud.Framework.Persistence.Abstractions.Tests.Sql.Interfaces
{
    public sealed class IDbContextTests
    {
        [Fact]
        public void CreateDbConnection_Tests_Succeeds() {
            // arrange
            var sut = Substitute.For<IDbContext>();
            sut.CreateDbConnection().Returns(new MySqlConnection());
            
            // act
            var actual = sut.CreateDbConnection();
            
            // assert
            actual.Should().NotBeNull()
                  .And
                  .BeAssignableTo<DbConnection>();
        }
    }
}