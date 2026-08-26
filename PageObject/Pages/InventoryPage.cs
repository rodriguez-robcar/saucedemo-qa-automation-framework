// <copyright file="InventoryPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.PageObject.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Class that contains all elements and methods of the InventoryPage.
    /// </summary>
    public class InventoryPage
    {
        private readonly IWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryPage"/> class.
        /// </summary>
        /// <param name="driver">WebDriver.</param>
        public InventoryPage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentException(nameof(driver));

        private IWebElement Title => this.driver.FindElement(By.XPath("//*[@class = 'app_logo']"));

        private IWebElement ProductSortContainer => this.driver.FindElement(By.XPath("//select[@class = 'product_sort_container']"));

        /// <summary>
        /// Method that returns the header element.
        /// </summary>
        /// <returns>Header web element.</returns>
        public IWebElement GetTitle()
        {
            return this.Title;
        }

        /// <summary>
        /// Gets the list of product names currently displayed on the page.
        /// </summary>
        /// <returns>Collection of product names.</returns>
        public List<string> GetDisplayedProductNames()
        {
            return this.driver.FindElements(By.ClassName("inventory_item_name")).Select(element => element.Text).ToList();
        }

        /// <summary>
        /// Gets the list of product prices currently displayed on the page.
        /// </summary>
        /// <returns>Collection of product prices.</returns>
        public List<decimal> GetDisplayedProductPrices()
        {
            return this.driver.FindElements(By.ClassName("inventory_item_price"))
        .Select(element => decimal.Parse(element.Text.Replace("$", string.Empty))).ToList();
        }

        /// <summary>
        /// Sorts products by price from low to high.
        /// </summary>
        /// <param name="value">Sort option value.</param>
        public void SelectSortOption(string value)
        {
            SelectElement selectElement = new SelectElement(this.ProductSortContainer);
            selectElement.SelectByValue(value);
        }
    }
}
