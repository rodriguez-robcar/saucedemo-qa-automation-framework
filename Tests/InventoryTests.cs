// <copyright file="InventoryTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.InventoryTests
{
    using FluentAssertions;
    using NLog;
    using OpenQA.Selenium;
    using SauceDemo.PageObject.Pages;
    using SauceDemo.Utils;

    /// <summary>
    /// Class that contains all test cases.
    /// </summary>
    [TestClass]
    public sealed class InventoryTests
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
        /// Sets webdriver and creates an instance of the LoginPage class before each test.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.Instance = WebDriverSingleton.GetInstance("chrome");
            this.Driver = this.Instance.GetDriver();
            this.LoginPage = new LoginPage(this.Driver);
            Logger.Info("Tests started.");

            this.InventoryPage = new InventoryPage(this.Driver);
            this.LoginPage.Open().LoginWithUsernameAndPassword("standard_user", "secret_sauce");
            Logger.Info("Logged in with standard_user credentials.");
        }

        /// <summary>
        /// Test sorting of products by price or name from low to high and high to low.
        /// </summary>
        /// <param name="sortValue">Sort option value.</param>
        /// <param name="field">Field to sort by.</param>
        /// <param name="descending">Boolean value to indicate if the sort is descending.</param
        [TestMethod]
        [DataRow("az", "Name", false)]
        [DataRow("za", "Name", true)]
        [DataRow("lohi", "Price", false)]
        [DataRow("hilo", "Price", true)]
        public void Sorting_ShouldOrderProductsCorrectly(string sortValue, string field, bool descending)
        {
            Logger.Info("VerifySortingOfProducts started.");

            Logger.Debug("Sorting products by " + field + " from " + (descending ? "high to low" : "low to high") + ".");
            this.InventoryPage.SelectSortOption(sortValue);

            Logger.Debug("Asserting that the products are sorted correctly.");
            if (field == "Name")
            {
                var actual = this.InventoryPage.GetDisplayedProductNames();
                TestAssertions.AssertSortCorrectly(actual, descending);
            }
            else
            {
                var actual = this.InventoryPage.GetDisplayedProductPrices();
                TestAssertions.AssertSortCorrectly(actual, descending);
            }

            Logger.Info("VerifySortingOfProducts finished.");
        }

        /// <summary>
        /// Test to verify that all product images are loaded correctly.
        /// </summary>
        [TestMethod]
        public void VerifyAllProductImagesAreLoaded()
        {
            Logger.Info("VerifyAllProductImagesAreLoaded started.");

            Logger.Debug("Getting the image load statuses for all products.");
            var imageLoadStatuses = this.InventoryPage.GetImageLoadStatuses();

            Logger.Debug("Asserting that all product images are loaded.");
            imageLoadStatuses.Should().OnlyContain(status => status, "All product images should be loaded.");

            Logger.Info("VerifyAllProductImagesAreLoaded finished.");
        }

        /// <summary>
        /// Test to verify that adding a product to the cart updates the cart badge and disables the 'Add to Cart' button.
        /// </summary>
        /// <param name="productName">Name of the product to add to the cart.</param>
        [TestMethod]
        [DataRow("backpack")]
        [DataRow("bike-light")]
        [DataRow("bolt-t-shirt")]

        public void AddToCart_ShouldUpdateBadgeAndButtonState(string productName)
        {
            Logger.Info("AddToCart_ShouldUpdateBadgeAndButtonState started.");

            Logger.Debug("Adding product to cart.");
            this.InventoryPage.AddToCart(productName);

            Logger.Debug("Asserting that the cart badge is updated.");
            var cartBadgeCount = this.InventoryPage.GetCartBadgeCount();
            cartBadgeCount.Should().Be(1, "Cart badge should show 1 item after adding a product to the cart.");

            Logger.Debug("Asserting that the 'Add to Cart' button is disabled.");
            var isRemoveButtonDisplayed = this.InventoryPage.IsRemoveButtonDisplayed(productName);
            isRemoveButtonDisplayed.Should().BeTrue("'Add to Cart' button should be disabled after adding the product to the cart.");

            Logger.Info("AddToCart_ShouldUpdateBadgeAndButtonState finished.");
        }

        /// <summary>
        /// Test to verify that adding multiple products to the cart updates the cart badge and disables the 'Add to Cart' buttons for those products.
        /// </summary>
        /// <param name="productNames">Array of product names to add to the cart.</param>
        [TestMethod]
        [DataRow(new string[] { "backpack", "bike-light", "bolt-t-shirt", "fleece-jacket", "onesie" })]
        public void AddMultipleItems_ShouldUpdateBadgeAndButtonState(string[] productNames)
        {
            Logger.Info("AddMultipleItems_ShouldUpdateBadgeAndButtonState started.");

            foreach (var productName in productNames)
            {
                Logger.Debug($"Adding product '{productName}' to cart.");
                this.InventoryPage.AddToCart(productName);
            }

            Logger.Debug("Asserting that the cart badge is updated.");
            var cartBadgeCount = this.InventoryPage.GetCartBadgeCount();
            cartBadgeCount.Should().Be(productNames.Length, $"Cart badge should show {productNames.Length} items after adding multiple products to the cart.");

            foreach (var productName in productNames)
            {
                Logger.Debug($"Asserting that the 'Add to Cart' button for '{productName}' is disabled.");
                var isRemoveButtonDisplayed = this.InventoryPage.IsRemoveButtonDisplayed(productName);
                isRemoveButtonDisplayed.Should().BeTrue($"'Add to Cart' button for '{productName}' should be disabled after adding the product to the cart.");
            }

            Logger.Info("AddMultipleItems_ShouldUpdateBadgeAndButtonState finished.");
        }

        /// <summary>
        /// Quits driver and sets instance to null after each test.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            this.Instance.QuitDriver();
            Logger.Info("Tests finished");
        }
    }
}
