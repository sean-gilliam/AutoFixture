﻿using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.AutoFixtureUnitTest.Kernel
{
    public class CriterionTests
    {
        [Fact]
        public void SutIsEquatable()
        {
            var sut = new Criterion<string>(
                "ploeh",
                new DelegatingEqualityComparer<string>());
            Assert.IsAssignableFrom<IEquatable<string>>(sut);
        }

        [Fact]
        public void ConstructWithNullTargetThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new Criterion<Version>(
                    null,
                    new DelegatingEqualityComparer<Version>()));
        }

        [Fact]
        public void ConstructWithNullComparerThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new Criterion<int>(42, null));
        }

        [Theory]
        [InlineData("ploeh", "ndøh", true)]
        [InlineData("fnaah", "sqryt", false)]
        public void EqualsReturnsComparerResult(
            string target,
            string candidate,
            bool expected)
        {
            var comparer = new DelegatingEqualityComparer<string>
            {
                OnEquals = (x, y) =>
                {
                    Assert.Equal(target, x);
                    Assert.Equal(candidate, y);
                    return expected;
                }
            };
            var sut = new Criterion<string>(target, comparer);

            var actual = sut.Equals(candidate);

            Assert.Equal(expected, actual);
        }
    }
}