// <copyright file="TestAssertions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace SauceDemo.Utils
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;

    /// <summary>
    /// Class that contains methods to set options for the specified browser.
    /// </summary>
    public static class TestAssertions
    {
        /// <summary>
        /// Asserts that the actual list is sorted correctly based on the specified order (ascending or descending).
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="actual">The actual list to be checked.</param>
        /// <param name="descending">A boolean indicating whether the list should be sorted in descending order (true) or ascending order (false).</param>
        public static void AssertSortCorrectly<T>(List<T> actual, bool descending)
        {
            var expected = descending
                ? actual.OrderByDescending(x => x).ToList()
                : actual.OrderBy(x => x).ToList();

            CollectionAssert.AreEqual(expected, actual, "The actual list is not sorted correctly.");
        }
    }
}