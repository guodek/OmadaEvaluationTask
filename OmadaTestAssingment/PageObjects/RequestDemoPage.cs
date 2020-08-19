using Faker;
using NUnit.Framework;
using OmadaTestAssingment.SeleniumHelpers;
using OpenQA.Selenium;

namespace OmadaTestAssingment.PageObjects
{
    internal class RequestDemoPage
    {
        protected readonly IWebDriver driver;
        private readonly WebExtensions extensions = new WebExtensions();

        public RequestDemoPage(IWebDriver driver)
        {
            this.driver = driver;
            By headlineSelector = By.XPath("//*[@class='headline__heading' and contains(text(),'Request an Omada Identity One-To-One Live Demo')]");
            IWebElement headline = driver.FindElement(headlineSelector);
            Assert.IsTrue(extensions.IsElementDisplayed(headline), "RequestDemo page is not displayed");
        }

        ///***********************
        ///***********************
        ///     PAGE ELEMENTS
        ///***********************
        ///***********************

        /// <summary>
        /// Input: "First Name"
        /// </summary>
        public IWebElement FirstNameTextBox => driver.FindElement(By.CssSelector("p.first_name>input"));

        /// <summary>
        /// Input: "Last Name"
        /// </summary>
        public IWebElement LastNameTextBox => driver.FindElement(By.CssSelector("p.last_name>input"));

        /// <summary>
        /// Input: "Company"
        /// </summary>
        public IWebElement CompanyTextBox => driver.FindElement(By.CssSelector("p.company>input"));

        /// <summary>
        /// Input: "Job title"
        /// </summary>
        public IWebElement JobTitleTextBox => driver.FindElement(By.CssSelector("p.job_title>input"));

        /// <summary>
        /// Input: "Business email"
        /// </summary>
        public IWebElement BusinessEmailTextBox => driver.FindElement(By.CssSelector("p.email>input"));

        /// <summary>
        /// Input: "Phone"
        /// </summary>
        public IWebElement PhoneTextBox => driver.FindElement(By.CssSelector("p.phone>input"));

        /// <summary>
        /// Select: "Country"
        /// </summary>
        public IWebElement CountryDropdown => driver.FindElement(By.CssSelector("p.country>select"));

        /// <summary>
        /// Select: "Number of employees"
        /// </summary>
        public IWebElement NoOfEmployeesDropdown => driver.FindElement(By.CssSelector("p.sf_Number_of_Employees>select"));

        /// <summary>
        /// Radiobutton: "Privacy policy"
        /// </summary>
        public IWebElement PrivacyPolicyRadioButton => driver.FindElement(By.CssSelector("p.Lead_Marketing_Permission>span>span"));

        /// <summary>
        /// Button: "Submit"
        /// </summary>
        public IWebElement SubmitFormButton => driver.FindElement(By.XPath("//input[@value='Submit']"));

        /// <summary>
        /// Paragraph: "Invalid Captcha"
        /// </summary>
        public IWebElement InvalidCaptchaParagraph => driver.FindElement(By.XPath("//p[text()='Invalid CAPTCHA']"));

        ///***********************
        ///***********************
        ///     METHODS
        ///***********************
        ///***********************

        /// <summary>
        /// Fills required fields in request demo form with fake data
        /// </summary>
        /// <returns>Request Demo page</returns>
        public RequestDemoPage FillRequestDemoForm()
        {
            SwitchToIFrame(0);
            FirstNameTextBox.SendKeys(Name.First());
            LastNameTextBox.SendKeys(Name.Last());
            CompanyTextBox.SendKeys(Company.Name());
            JobTitleTextBox.SendKeys("Test employee");
            BusinessEmailTextBox.SendKeys(Internet.Email());
            PhoneTextBox.SendKeys(Phone.Number());
            extensions.SelectDropdownByText(CountryDropdown, "Poland");
            extensions.SelectDropdownByText(NoOfEmployeesDropdown, "0-500");
            PrivacyPolicyRadioButton.Click();

            return this;
        }

        /// <summary>
        /// Submits filled form without captcha, checks if 'Invalid CAPTCHA' error message is displayed.
        /// </summary>
        /// <returns>Request Demo Page</returns>
        public RequestDemoPage SubmitFilledDemoRequestWithoutCaptchaAndCheckError()
        {
            FillRequestDemoForm();
            SubmitFormButton.Submit();
            Assert.IsTrue(extensions.IsElementDisplayed(InvalidCaptchaParagraph), "'Invalid captcha' paragraph is not displayed");

            return this;
        }

        /// <summary>
        /// Submits empty contact form, checks if correct number of 'Field required' error messages is displayed.
        /// </summary>
        /// <param name="numberOfRequiredPopUps">Number of times, that 'Field required' error message should be displayed</param>
        /// <returns>Contact page</returns>
        public RequestDemoPage SubmitEmptyFormAndCheckRequiredInfo(int numberOfRequiredErrors)
        {
            SwitchToIFrame(0);
            SubmitFormButton.Submit();
            By fieldRequiredPopUpSelector = By.XPath("//p[contains(text(),'This field is required')]");
            int popUpCount = driver.FindElements(fieldRequiredPopUpSelector).Count;
            Assert.IsTrue(popUpCount == numberOfRequiredErrors, $"Number of 'This field is required' pop-up messages ({popUpCount}) is different than expected ({numberOfRequiredErrors}).'");

            return this;
        }

        /// <summary>
        /// Switches WebDriver to iframe selected by index number
        /// </summary>
        /// <param name="frameIndex">Index of the iframe to switch to</param>
        private void SwitchToIFrame(int frameIndex)
        {
            driver.SwitchTo().Frame(frameIndex);
        }
    }
}