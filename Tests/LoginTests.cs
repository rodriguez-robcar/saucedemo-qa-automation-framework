// <copyright file="LoginTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.LoginTests
{
    using FluentAssertions;
    using NLog;
    using SauceDemo.PageObject.Pages;

    /// <summary>
    /// Class that contains all login test cases.
    /// </summary>
    [TestClass]
    public class LoginTests : BaseTest
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Sets webdriver and creates an instance of the LoginPage class before each test.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Test Login form with empty credentials.
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        [TestMethod]
        [DataRow("test", "test")]
        public void LoginWithEmptyCredentials(string username, string password)
        {
            Logger.Info("LoginWithEmptyCredentials started.");
            this.LoginPage.Open().LoginWithEmptyCredentials(username, password);

            Logger.Debug("Asserting error message.");
            this.LoginPage.GetErrorMessage().Text.Should().Contain("Username is required");

            Logger.Info("LoginWithEmptyCredentials finished.");
        }

        /// <summary>
        /// Test Login form with credentials by passing only the username.
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        [TestMethod]
        [DataRow("test", "test")]
        public void LoginWithEmptyPassword(string username, string password)
        {
            Logger.Info("LoginWithEmptyPassword started.");
            this.LoginPage.Open().LoginWithEmptyPassword(username, password);

            Logger.Debug("Asserting error message.");
            this.LoginPage.GetErrorMessage().Text.Should().Contain("Password is required");

            Logger.Info("LoginWithEmptyPassword finished.");
        }

        /// <summary>
        /// Test Login form with credentials by passing Username & Password.
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        [TestMethod]
        [DataRow("standard_user", "secret_sauce")]
        public void LoginWithUsernameAndPassword(string username, string password)
        {
            Logger.Info("LoginWithUsernameAndPassword started.");
            this.InventoryPage = new InventoryPage(this.Driver);
            this.LoginPage.Open().LoginWithUsernameAndPassword(username, password);

            Logger.Debug("Asserting title.");
            this.InventoryPage.GetTitle().Text.Should().Contain("Swag Labs");

            Logger.Info("LoginWithUsernameAndPassword finished.");
        }
    }
}
