using Cloud.Framework.Persistence.Abstractions.Sql.Interfaces;
using FluentAssertions;
using MySql.Data.MySqlClient;
using NSubstitute;
using Xunit;

namespace Cloud.Framework.Persistence.Abstractions.Tests.Sql.Interfaces
{
    public sealed class IDbSettingsTests
    {
        [Fact]
        public void CreateDbConnection_Tests_Succeeds() {
            // arrange
            var sut = Substitute.For<IDbSettings<MySqlConnection>>();
            sut.CreateDbConnection().Returns(new MySqlConnection());
            
            // act
            var actual = sut.CreateDbConnection();
            
            // assert
            actual.Should().NotBeNull()
                  .And
                  .BeOfType<MySqlConnection>();
        }
    }
}