// <copyright file="WebDriverSingleton.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.Utils
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;

    /// <summary>
    /// WebDriver singleton implementation.
    /// </summary>
    public class WebDriverSingleton
    {
        private static WebDriverSingleton? instance;

        private readonly IWebDriver driver;

        private WebDriverSingleton(string value)
        {
            switch (value)
            {
                case "Chrome":
                case "chrome":
                    this.driver = new ChromeDriver(WebDriverOptions.GetChromeOptions());
                    break;
                case "Edge":
                case "edge":
                    this.driver = new EdgeDriver(WebDriverOptions.GetEdgeOptions());
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unsupported or incorrect browser");
            }
        }

        /// <summary>
        /// Method that returns an instance of the WebDriverSingleton class.
        /// </summary>
        /// <param name="browser">Name of browser.</param>
        /// <returns>Instance of WebDriverSingleton class.</returns>
        public static WebDriverSingleton GetInstance(string browser)
        {
            if (instance == null)
            {
                instance = new WebDriverSingleton(browser);
            }

            return instance;
        }

        /// <summary>
        /// Method that returns the webdriver.
        /// </summary>
        /// <returns>Web driver of the selected browser.</returns>
        public IWebDriver GetDriver()
        {
            return this.driver;
        }

        /// <summary>
        /// Quits driver and sets instance to null.
        /// </summary>
        public void QuitDriver()
        {
            if (this.driver != null)
            {
                this.driver.Quit();
                instance = null;
            }
        }
    }
}
