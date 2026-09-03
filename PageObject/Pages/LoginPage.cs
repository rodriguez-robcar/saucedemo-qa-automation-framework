// <copyright file="LoginPage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SauceDemo.PageObject.Pages
{
    using System;
    using System.Runtime.InteropServices;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Class that contains all elements and methods of the LoginPage.
    /// </summary>
    public class LoginPage : BasePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="driver">Constructor receives the webdriver as parameter.</param>
        public LoginPage(IWebDriver driver)
            : base(driver)
        {
        }

        private static string Url { get; } = "https://www.saucedemo.com/";

        private IWebElement UsernameField => this.WaitAndFind(By.XPath("//input[@id = 'user-name']"));

        private IWebElement PasswordField => this.WaitAndFind(By.XPath("//input[@id = 'password']"));

        private IWebElement LoginButton => this.WaitAndFind(By.XPath("//input[@id = 'login-button']"));

        private IWebElement ErrorMessage => this.WaitAndFind(By.XPath("//*[@data-test = 'error']"));

        /// <summary>
        /// Opens url and returns an instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <returns>Instance of the LoginPage class.</returns>
        public LoginPage Open()
        {
            this.Driver.Navigate().GoToUrl(Url);
            return this;
        }

        /// <summary>
        /// Method to test Login form with empty credentials.
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        public void LoginWithEmptyCredentials(string username, string password)
        {
            this.UsernameField.SendKeys(username);
            this.PasswordField.SendKeys(password);

            var clearFieldWait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(10));

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                this.UsernameField.SendKeys(Keys.Command + "a");
                this.UsernameField.SendKeys(Keys.Backspace);

                clearFieldWait.Until(driver =>
                    string.IsNullOrEmpty(this.UsernameField.GetAttribute("value")));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                this.UsernameField.SendKeys(Keys.Control + "a");
                this.UsernameField.SendKeys(Keys.Delete);

                clearFieldWait.Until(driver => this.UsernameField.GetAttribute("value") == string.Empty);
            }

            this.LoginButton.Click();
        }

        /// <summary>
        /// Method to test Login form with credentials by passing only the username.
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        public void LoginWithEmptyPassword(string username, string password)
        {
            this.UsernameField.SendKeys(username);
            this.PasswordField.SendKeys(password);

            var clearFieldWait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(10));

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                this.PasswordField.SendKeys(Keys.Command + "a");
                this.PasswordField.SendKeys(Keys.Backspace);

                clearFieldWait.Until(driver =>
                string.IsNullOrEmpty(this.PasswordField.GetAttribute("value")));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                this.PasswordField.SendKeys(Keys.Control + "a");
                this.PasswordField.SendKeys(Keys.Delete);

                clearFieldWait.Until(driver => this.PasswordField.GetAttribute("value") == string.Empty);
            }

            this.LoginButton.Click();
        }

        /// <summary>
        /// Method to test Login form with credentials by passing Username & Password.
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        public void LoginWithUsernameAndPassword(string username, string password)
        {
            this.UsernameField.SendKeys(username);
            this.PasswordField.SendKeys(password);
            this.LoginButton.Click();
        }

        /// <summary>
        /// Returns the error message shown after a failed login.
        /// </summary>
        /// <returns>Error message element.</returns>
        public IWebElement GetErrorMessage()
        {
            return this.ErrorMessage;
        }
    }
}
