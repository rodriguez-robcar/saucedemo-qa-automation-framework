// <copyright file="ProductDetailPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.PageObject.Pages
{
    using OpenQA.Selenium;

    /// <summary>
    /// Class that contains all elements and methods of the ProductDetailPage.
    /// </summary>
    public class ProductDetailPage : BasePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailPage"/> class.
        /// </summary>
        /// <param name="driver">WebDriver.</param>
        public ProductDetailPage(IWebDriver driver)
            : base(driver)
        {
        }

        private IWebElement AddToCartButton => this.WaitAndFind(By.XPath("//button[@id = 'add-to-cart']"));

        private IWebElement RemoveFromCartButton => this.WaitAndFind(By.XPath("//button[@id = 'remove']"));

        /// <summary>
        /// Method that clicks the back to products button.
        /// </summary>
        public void ClickBackToProductsButton()
        {
            this.WaitAndFind(By.Id("back-to-products")).Click();
        }

        /// <summary>
        /// Gets the name of the product currently displayed on the page.
        /// </summary>
        /// <returns>Product name.</returns>
        public string GetProductName()
        {
            return this.WaitAndFind(By.ClassName("inventory_details_name")).Text;
        }

        /// <summary>
        /// Gets the price of the product currently displayed on the page.
        /// </summary>
        /// <returns>Product price.</returns>
        public string GetProductPrice()
        {
            return this.WaitAndFind(By.ClassName("inventory_details_price")).Text;
        }

        /// <summary>
        /// Clicks the 'Add to Cart' button on the product detail page.
        /// </summary>
        public void ClickAddToCartButton()
        {
            this.AddToCartButton.Click();
        }

        /// <summary>
        /// Clicks the 'Remove' button on the product detail page.
        /// </summary>
        public void ClickRemoveFromCartButton()
        {
            this.RemoveFromCartButton.Click();
        }

        /// <summary>
        /// Checks if the 'Remove' button is displayed on the product detail page.
        /// </summary>
        /// <returns>True if the button is displayed, false otherwise.</returns>
        public bool IsRemoveButtonDisplayed()
        {
            return this.IsElementDisplayed(By.XPath("//button[@id = 'remove']"));
        }

        /// <summary>
        /// Checks if the 'Add to Cart' button is displayed on the product detail page.
        /// </summary>
        /// <returns>True if the button is displayed, false otherwise.</returns>
        public bool IsAddToCartButtonDisplayed()
        {
            return this.IsElementDisplayed(By.XPath("//button[@id = 'add-to-cart']"));
        }

        /// <summary>
        /// Waits for the product detail page to fully load.
        /// </summary>
        public void WaitForPageLoad()
        {
            this.WaitAndFind(By.ClassName("inventory_details_name"));
        }
    }
}