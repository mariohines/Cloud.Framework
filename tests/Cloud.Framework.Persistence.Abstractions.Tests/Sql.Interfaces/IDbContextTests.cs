using System.Data.Common;
using System.Threading.Tasks;
using Cloud.Framework.Persistence.Abstractions.Sql.Interfaces;
using FluentAssertions;
using MySql.Data.MySqlClient;
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
                  .And.BeOfType<DbCommand>();
        }

        [Fact]
        public async Task CreateDbTransaction_Tests_Succeeds() {
            // arrange
            var sut = Substitute.For<IDbContext>();
            sut.CreateDbTransactionAsync()
               .Returns(c => {
                            var connection = new MySqlConnection();
                            return connection.BeginTransaction();
                        });
            
            // act
            var actual = await sut.CreateDbTransactionAsync();
            
            // assert
            actual.Should().NotBeNull();
        }
    }
}