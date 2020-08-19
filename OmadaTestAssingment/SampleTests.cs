using NUnit.Framework;
using OmadaTestAssingment.PageObjects;
using OmadaTestAssingment.SeleniumHelpers;
using OpenQA.Selenium;

namespace OmadaTestAssingment
{
    public class SampleTests
    {
        private IWebDriver driver;

        /// Config used in test execution
        /// available web drivers: Chrome, Firefox
        private readonly string driverToUse = "Chrome";
        private readonly int pageLoadTimeout = 60;
        private readonly int implicitWaitTime = 30;
        private readonly string baseUrl = "http://www.omada.net";

        [SetUp]
        public void Setup()
        {
            driver = new DriverFactory().Create(driverToUse, pageLoadTimeout, implicitWaitTime);
            driver.Navigate().GoToUrl(baseUrl);
            new MainPage(driver).CloseCookieBar();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void CheckIfEmptyDemoRequestFormIsNotSent()
        {
            new MainPage(driver)
                .GoToRequestDemoPage()
                .SubmitEmptyFormAndCheckRequiredInfo(9);
        }

        [Test]
        public void CheckIfFilledDemoRequestWithoutCaptchaIsNotSent()
        {
            new MainPage(driver)
                .GoToRequestDemoPage()
                .SubmitFilledDemoRequestWithoutCaptchaAndCheckError();
        }

        [Test]
        public void CheckIfEmptyContactFormIsNotSent()
        {
            new MainPage(driver)
                .GoToContactPage()
                .SubmitEmptyFormAndCheckRequiredInfo(10);
        }

        [Test]
        public void CheckIfFilledContactFormWithoutCaptchaIsNotSent()
        {
            new MainPage(driver)
                .GoToContactPage()
                .SubmitFilledDemoRequestWithoutCaptchaAndCheckError();
        }
    }
}