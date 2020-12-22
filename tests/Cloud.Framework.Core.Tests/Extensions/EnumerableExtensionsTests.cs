using System.Collections;
using System.Collections.Generic;
using Cloud.Framework.Core.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Cloud.Framework.Core.Tests.Extensions
{
    public sealed class EnumerableExtensionsTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        
        public EnumerableExtensionsTests(ITestOutputHelper testOutputHelper) {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [ClassData(typeof(AsBatchTestData))]
        public void EnumerableExtensions_AsBatch_Returns_Expected(IEnumerable<string> sut, int batchSize, int expected) {
            // act
            foreach (var batch in sut.AsBatch(batchSize)) {
                // assert
                batch.Should().HaveCount(expected);
            }
        }
        
        private sealed class AsBatchTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {new[] {"A", "B", "C", "D"}, 1, 1};
                yield return new object[] {new[] {"A", "B", "C", "D"}, 2, 2};
                yield return new object[] {new[] {"A", "B", "C", "D", "E", "F"}, 3, 3};
                yield return new object[] {new[] {"A", "B", "C", "D", "E", "F", "G", "H"}, 4, 4};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}