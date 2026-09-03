// <copyright file="BaseTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.PageObject.Pages
{
    using NLog;
    using OpenQA.Selenium;
    using SauceDemo.Utils;

    /// <summary>
    /// Class that contains all elements and methods of the base test class.
    /// </summary>
    public abstract class BaseTest
    {
        /// <summary>
        /// Instance field.
        /// </summary>
        required public WebDriverSingleton Instance;

        /// <summary>
        /// Driver field.
        /// </summary>
        required public IWebDriver Driver;

        /// <summary>
        /// LoginPage field.
        /// </summary>
        required public LoginPage LoginPage;

        /// <summary>
        /// InventoryPage field.
        /// </summary>
        required public InventoryPage InventoryPage;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        /// <param name="driver">WebDriver.</param>
        [TestInitialize]
        public virtual void Initialize()
        {
            this.Instance = WebDriverSingleton.GetInstance("chrome");
            this.Driver = this.Instance.GetDriver();
            this.LoginPage = new LoginPage(this.Driver);
            Logger.Info("Tests started.");
        }

        /// <summary>
        /// Quits driver and sets instance to null after each test.
        /// </summary>
        [TestCleanup]
        public virtual void Cleanup()
        {
            this.Instance.QuitDriver();
            Logger.Info("Tests finished");
        }
    }
}