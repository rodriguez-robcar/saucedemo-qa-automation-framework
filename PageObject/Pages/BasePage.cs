// <copyright file="BasePage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.PageObject.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// Class that contains all elements and methods of the BasePage.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="BasePage"/> class.
    /// </remarks>
    /// <param name="driver">WebDriver.</param>
    public class BasePage(IWebDriver driver)
    {
        /// <summary>
        /// Gets the WebDriver used by the page.
        /// </summary>
        private readonly IWebDriver driver = driver ?? throw new ArgumentException(nameof(driver));

        /// <summary>
        /// Gets the WebDriverWait used by the page.
        /// </summary>
        private readonly WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        /// <summary>
        /// Gets the WebDriver used by the page.
        /// </summary>
        protected IWebDriver Driver => this.driver;

        /// <summary>
        /// Returns a web element after waiting for it to be visible on the page.
        /// </summary>
        /// <param name="locator">Locator of the web element.</param>
        /// <returns>Web element.</returns>
        public IWebElement WaitAndFind(By locator)
        {
            return this.wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        /// <summary>
        /// Checks if an element is displayed on the page within a specified timeout.
        /// </summary>
        /// <param name="locator">Locator of the web element.</param>
        /// <param name="timeoutInSeconds">Timeout in seconds.</param>
        /// <returns>True if the element is displayed, false otherwise.</returns>
        public bool IsElementDisplayed(By locator, int timeoutInSeconds = 10)
        {
            try
            {
                var shortWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return shortWait.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}