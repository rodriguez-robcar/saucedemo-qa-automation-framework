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
    public class InventoryPage : BasePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryPage"/> class.
        /// </summary>
        /// <param name="driver">WebDriver.</param>
        public InventoryPage(IWebDriver driver)
            : base(driver)
        {
        }

        private IWebElement ProductSortContainer => this.WaitAndFind(By.XPath("//select[@class = 'product_sort_container']"));

        /// <summary>
        /// Method that returns the header element.
        /// </summary>
        /// <returns>Header web element.</returns>
        public IWebElement GetTitle()
        {
            return this.WaitAndFind(By.XPath("//*[@class = 'app_logo']"));
        }

        /// <summary>
        /// Method that returns true if the product sort container element is displayed.
        /// </summary>
        /// <returns>Boolean indicating if the product sort container is displayed.</returns>
        public bool GetProductSortContainer()
        {
            return this.ProductSortContainer.Displayed;
        }

        /// <summary>
        /// Gets the list of product names currently displayed on the page.
        /// </summary>
        /// <returns>Collection of product names.</returns>
        public List<string> GetDisplayedProductNames()
        {
            return this.WaitAndFindAll(By.ClassName("inventory_item_name")).Select(element => element.Text).ToList();
        }

        /// <summary>
        /// Gets the list of product prices currently displayed on the page.
        /// </summary>
        /// <returns>Collection of product prices.</returns>
        public List<decimal> GetDisplayedProductPrices()
        {
            return this.WaitAndFindAll(By.ClassName("inventory_item_price"))
        .Select(element => decimal.Parse(element.Text.Replace("$", string.Empty))).ToList();
        }

        /// <summary>
        /// Gets the list of image load statuses for products currently displayed on the page.
        /// </summary>
        /// <returns>Collection of image load statuses.</returns>
        public List<bool> GetImageLoadStatuses()
        {
            var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElements(By.CssSelector("img.inventory_item_img")).Count > 0);

            var images = this.Driver.FindElements(By.CssSelector("img.inventory_item_img")).ToList();

            var jsExecutor = (IJavaScriptExecutor)this.Driver;

            return images.Select(img =>
            {
                try
                {
                    wait.Until(d =>
                        Convert.ToInt64(jsExecutor.ExecuteScript("return arguments[0].naturalWidth", img)) > 0);
                    return true;
                }
                catch (WebDriverTimeoutException)
                {
                    return false;
                }
            }).ToList();
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

        /// <summary>
        /// Method to add a product to the cart by its name.
        /// </summary>
        /// <param name="productName">Name of the product to add.</param>
        public void AddToCart(string productName)
        {
            var locator = By.XPath("//button[@id = 'add-to-cart-sauce-labs-" + productName.Replace(" ", "-").ToLower() + "']");
            this.WaitAndFindClickable(locator).Click();
        }

        /// <summary>
        /// Method to remove a product from the cart by its name.
        /// </summary>
        /// <param name="productName">Name of the product to remove.</param>
        public void RemoveFromCart(string productName)
        {
            var locator = By.XPath("//button[@id = 'remove-sauce-labs-" + productName.Replace(" ", "-").ToLower() + "']");
            this.WaitAndFindClickable(locator).Click();
        }

        /// <summary>
        /// Checks if the remove button for a product is displayed.
        /// </summary>
        /// <param name="productName">Name of the product to check.</param>
        /// <returns>True if the remove button is displayed, otherwise false.</returns>
        public bool IsRemoveButtonDisplayed(string productName)
        {
            var locator = By.XPath("//button[@id ='remove-sauce-labs-" + productName.Replace(" ", "-").ToLower() + "']");
            return this.IsElementDisplayed(locator);
        }

        /// <summary>
        /// Checks if the add to cart button for a product is displayed.
        /// </summary>
        /// <param name="productName">Name of the product to check.</param>
        /// <returns>True if the add to cart button is displayed, otherwise false.</returns
        public bool IsAddToCartButtonDisplayed(string productName)
        {
            var locator = By.XPath("//button[@id ='add-to-cart-sauce-labs-" + productName.Replace(" ", "-").ToLower() + "']");
            return this.IsElementDisplayed(locator);
        }

        /// <summary>
        /// Gets the count of items in the shopping cart badge.
        /// </summary>
        /// <returns>Count of items in the shopping cart badge.</returns>
        public int GetCartBadgeCount()
        {
            try
            {
                var badgeElement = this.WaitAndFind(By.ClassName("shopping_cart_badge"));
                return int.TryParse(badgeElement.Text, out var count) ? count : 0;
            }
            catch (WebDriverTimeoutException)
            {
                return 0;
            }
        }

        /// <summary>
        /// Opens the product detail page for a specific product by its name.
        /// </summary>
        /// <param name="productName">Name of the product to open.</param>
        public void OpenProductDetailPage(string productName)
        {
            var locator = By.XPath($"//div[@class='inventory_item_name' and text()='{productName}']");
            this.WaitAndFindClickable(locator).Click();
        }

        /// <summary>
        /// Gets the product name by its index in the inventory list.
        /// </summary>
        /// <param name="index">Index of the product.</param>
        /// <returns>Product name.</returns>
        public string GetProductNameByIndex(int index)
        {
            return this.Driver.FindElements(By.ClassName("inventory_item_name"))[index].Text;
        }

        /// <summary>
        /// Clicks on a product by its index in the inventory list to open its detail page.
        /// </summary>
        /// <param name="index">Index of the product to click.</param>
        public void ClickProductByIndex(int index)
        {
            var products = this.WaitAndFindAll(By.ClassName("inventory_item_name"));
            var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(products.ElementAt(index))).Click();
        }
    }
}
