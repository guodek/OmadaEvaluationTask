using NUnit.Framework;
using OmadaTestAssingment.SeleniumHelpers;
using OpenQA.Selenium;

namespace OmadaTestAssingment.PageObjects
{
    internal class MainPage
    {
        protected readonly IWebDriver driver;
        private readonly WebExtensions extensions = new WebExtensions();

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
            IWebElement slider = driver.FindElement(By.CssSelector("div.slider--variant4"));
            Assert.IsTrue(extensions.IsElementDisplayed(slider), "Page is not loaded");
        }

        /// <summary>
        /// Button: "Book a demo" - Call To Action
        /// </summary>
        public IWebElement BookDemoCTAButton => driver.FindElement(By.XPath("//*[contains(@class, 'slider__button') and text() = 'Book a Demo']"));

        /// <summary>
        /// Link: "Contact" - Header link
        /// </summary>
        public IWebElement ContactLink => driver.FindElement(By.XPath("//a[@class='header__menulink--function-nav' and text() = 'Contact']"));

        /// <summary>
        /// Button: "Close" - Cookie bar
        /// </summary>
        public IWebElement CookieCloseButton => driver.FindElement(By.CssSelector("span.cookiebar__button.button--variant1"));

        /// <summary>
        /// Closes the Cookie Bar by pressing the 'Close' button.
        /// </summary>
        /// <returns>Main Page</returns>
        public MainPage CloseCookieBar()
        {
            CookieCloseButton.Click();

            return this;
        }

        /// <summary>
        /// Navigates to 'Request Demo' page by clicking the 'Book demo' button on the slider.
        /// </summary>
        /// <returns>Request Demo page</returns>
        public RequestDemoPage GoToRequestDemoPage()
        {
            BookDemoCTAButton.Click();

            return new RequestDemoPage(driver);
        }

        /// <summary>
        /// Navigates to 'Contact' page by clicking the 'Contact' link in the header.
        /// </summary>
        /// <returns>Contact page</returns>
        public ContactPage GoToContactPage()
        {
            ContactLink.Click();

            return new ContactPage(driver);
        }
    }
}