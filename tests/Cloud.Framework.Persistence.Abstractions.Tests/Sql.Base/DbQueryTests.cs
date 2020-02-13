using System;
using System.Data;
using System.Threading;
using Cloud.Framework.Persistence.Abstractions.Sql.Base;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Persistence.Abstractions.Tests.Sql.Base
{
    public sealed class DbQueryTests
    {
        [Fact]
        public void DbQuery_Create_Test_Succeeds() {
            // arrange
            const string sql = "select * from tests;";
            
            // act
            var sut = DbQuery.Create(sql);
            
            // assert
            sut.Parameters.Should().BeNull();
            sut.Token.Should().Be(default(CancellationToken));
            sut.Type.Should().Be(CommandType.Text);
            sut.Sql.Should().NotBeNullOrWhiteSpace()
               .And
               .Be(sql);
        }

        [Fact]
        public void DbQuery_Create_Test_Fails_On_Null_String() {
            // act
            Action sut = () => DbQuery.Create(null);
            
            // assert
            sut.Should().Throw<ArgumentException>()
               .And.ParamName.Should().Be("sql");
        }

        [Fact]
        public void DbQuery_Create_Test_Fails_On_Empty_String() {
            // act
            Action sut = () => DbQuery.Create(string.Empty);
            
            // assert
            sut.Should().Throw<ArgumentException>()
               .And.ParamName.Should().Be("sql");
        }
    }
}