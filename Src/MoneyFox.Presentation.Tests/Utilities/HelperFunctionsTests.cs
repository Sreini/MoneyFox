﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using MoneyFox.Presentation.Utilities;
using Should;
using Xunit;

namespace MoneyFox.Presentation.Tests.Utilities
{
    [ExcludeFromCodeCoverage]
    public class HelperFunctionsTests
    {
        [Fact]
        public void GetEndOfMonth_NoneInput_LastDayOfMonth()
        {
            HelperFunctions.GetEndOfMonth().ShouldBeType(typeof(DateTime));
        }

        [Theory]
        [InlineData(6000000.45)]
        [InlineData(6000000)]
        [InlineData(6000000.4567)]
        public void FormatLargeNumbers_ValidString(decimal amount)
        {
            HelperFunctions.FormatLargeNumbers(amount).ShouldEqual(amount.ToString("N"));
        }

        [Theory]
        [InlineData("10'000", "10'000", "de-CH")]
        [InlineData("10000", "10000", "de-CH")]
        [InlineData("10'000.50", "10000.50", "de-CH")]
        [InlineData("0.05", "0.05", "de-CH")]
        [InlineData("10'000", "10'000", "de-DE")]
        [InlineData("10000", "10000", "de-DE")]
        [InlineData("10'000.50", "10000,50", "de-DE")]
        [InlineData("0.05", "0,05", "de-DE")]
        [InlineData("10'000", "10'000", "en-US")]
        [InlineData("10000", "10000", "en-US")]
        [InlineData("10'000.50", "10000.50", "en-US")]
        [InlineData("0.05", "0.05", "en-US")]
        public void RemoveGroupingSeparators(string amount, string expectedResult, string culture)
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture, false);

            // Act / Assert
            HelperFunctions.RemoveGroupingSeparators(amount).ShouldEqual(expectedResult);
        }
    }
}