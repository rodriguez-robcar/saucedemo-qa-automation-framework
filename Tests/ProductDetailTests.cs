// <copyright file="ProductDetailTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.ProductDetailTests
{
    using FluentAssertions;
    using NLog;
    using SauceDemo.PageObject.Pages;

    /// <summary>
    /// Class that contains all elements and methods of the ProductDetailPage.
    /// </summary>
    [TestClass]
    public class ProductDetailTests : BaseTest
    {
        /// <summary>
        /// Gets or sets the ProductDetailPage instance.
        /// </summary>
        required public ProductDetailPage ProductDetailPage;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Sets webdriver and creates an instance of the LoginPage class before each test.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.InventoryPage = new InventoryPage(this.Driver);
            this.ProductDetailPage = new ProductDetailPage(this.Driver);
            this.LoginPage.Open().LoginWithUsernameAndPassword("standard_user", "secret_sauce");
            Logger.Info("Logged in with standard_user credentials.");
        }

        /// <summary>
        /// Test to verify that the product name on the inventory page matches the product name on the product detail page.
        /// </summary>
        /// <param name="productIndex">Index of the product to test.</param>

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void ProductName_ShouldMatchBetweenInventoryAndProductDetailPages(int productIndex)
        {
            Logger.Info("Starting test: ProductName_ShouldMatchBetweenInventoryAndProductDetailPages");

            Logger.Debug("Getting product name from inventory page.");
            var nameOnListing = this.InventoryPage.GetProductNameByIndex(productIndex);
            this.InventoryPage.ClickProductByIndex(productIndex);
            this.ProductDetailPage.GetProductName();

            Logger.Debug("Getting product name from product detail page.");
            var productNameOnProductDetailPage = this.ProductDetailPage.GetProductName();

            Logger.Debug("Asserting that the product names match.");
            nameOnListing.Should().Be(productNameOnProductDetailPage);
        }

        /// <summary>
        /// Test to verify that the add and remove functionality works correctly on the product detail page.
        /// </summary>
        /// <param name="productIndex">Index of the product to test.</param>
        [TestMethod]
        public void ProductDetailPage_ShouldAddAndRemoveProductFromCart()
        {
            Logger.Info("Starting test: ProductDetailPage_ShouldAddAndRemoveProductFromCart");

            Logger.Debug("Getting product name from inventory page.");
            this.InventoryPage.ClickProductByIndex(0);

            Logger.Debug("Clicking 'Add to Cart' button on product detail page.");
            this.ProductDetailPage.ClickAddToCartButton();
            this.ProductDetailPage.IsRemoveButtonDisplayed();

            Logger.Debug("Verifying that the product was added to the cart.");
            this.ProductDetailPage.IsRemoveButtonDisplayed().Should().BeTrue();
            this.InventoryPage.GetCartBadgeCount().Should().Be(1);

            Logger.Debug("Clicking 'Remove' button on product detail page.");
            this.ProductDetailPage.ClickRemoveFromCartButton();

            Logger.Debug("Verifying that the product was removed from the cart.");
            this.ProductDetailPage.IsAddToCartButtonDisplayed().Should().BeTrue();
            this.InventoryPage.GetCartBadgeCount().Should().Be(0);
        }

        /// <summary>
        /// Test to verify that clicking the 'Back to Products' button on the product detail page navigates the user back to the inventory page.
        /// </summary>
        [TestMethod]
        public void ProductDetailPage_ShouldNavigateBackToInventoryPage()
        {
            Logger.Info("Starting test: ProductDetailPage_ShouldNavigateBackToInventoryPage");

            Logger.Debug("Clicking on the first product to navigate to its detail page.");
            this.InventoryPage.ClickProductByIndex(0);

            Logger.Debug("Clicking 'Back to Products' button on product detail page.");
            this.ProductDetailPage.ClickBackToProductsButton();

            Logger.Debug("Verifying that the user is navigated back to the inventory page.");
            this.InventoryPage.GetProductSortContainer().Should().BeTrue("User should be navigated back to the inventory page.");
        }
    }
}