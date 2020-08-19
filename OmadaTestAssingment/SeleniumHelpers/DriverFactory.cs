using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace OmadaTestAssingment.SeleniumHelpers
{
    internal class DriverFactory
    {

        /// <summary>
        /// Checks if provided element is displayed on the page
        /// </summary>
        /// <param name="element">Element that needs to be checked</param>
        /// <returns>Bool - Is element displayed</returns>
        public IWebDriver Create(string driver, int pageLoadTime, int implicitWaitTime)
        {
            var driverToUse = driver; //ConfigurationManager.AppSettings["Driver"];
            IWebDriver selectedDriver = driverToUse switch
            {
                "Chrome" => new ChromeDriver(),
                "Firefox" => new FirefoxDriver(),
                _ => throw new ArgumentOutOfRangeException(),
            };
            selectedDriver.Manage().Window.Maximize();
            var timeouts = selectedDriver.Manage().Timeouts();

            timeouts.ImplicitWait = TimeSpan.FromSeconds(implicitWaitTime);
            timeouts.PageLoad = TimeSpan.FromSeconds(pageLoadTime);

            return selectedDriver;
        }
    }
}