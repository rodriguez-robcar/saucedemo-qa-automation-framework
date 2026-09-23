// <copyright file="BaseTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Tests
{
    using NLog;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using SauceDemo.PageObject.Pages;
    using SauceDemo.Utils;

    /// <summary>
    /// Class that contains all elements and methods of the base test class.
    /// </summary>
    public abstract class BaseTest
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets or sets instance field.
        /// </summary>
        required public WebDriverSingleton Instance { get; set; }

        /// <summary>
        /// Gets or sets driver field.
        /// </summary>
        required public IWebDriver Driver { get; set; }

        /// <summary>
        /// Gets or sets loginPage field.
        /// </summary>
        required public LoginPage LoginPage { get; set; }

        /// <summary>
        /// Gets or sets inventoryPage field.
        /// </summary>
        required public InventoryPage InventoryPage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        /// <param name="driver">WebDriver.</param>
        [TestInitialize]
        public virtual void Initialize()
        {
            // this.Instance = WebDriverSingleton.GetInstance("chrome");
            // this.Driver = this.Instance.GetDriver();
            this.Driver = new ChromeDriver(WebDriverOptions.GetChromeOptions());
            this.LoginPage = new LoginPage(this.Driver);
            Logger.Info("Tests started.");
        }

        /// <summary>
        /// Quits driver and sets instance to null after each test.
        /// </summary>
        [TestCleanup]
        public virtual void Cleanup()
        {
            // this.Instance.QuitDriver();
            this.Driver.Quit();
            Logger.Info("Tests finished");
        }
    }
}