using Faker;
using NUnit.Framework;
using OmadaTestAssingment.SeleniumHelpers;
using OpenQA.Selenium;

namespace OmadaTestAssingment.PageObjects
{
    internal class ContactPage
    {
        protected readonly IWebDriver driver;
        private readonly WebExtensions extensions = new WebExtensions();

        public ContactPage(IWebDriver driver)
        {
            this.driver = driver;
            IWebElement headline = driver.FindElement(By.XPath("//h1[text()='Contact Omada']"));
            Assert.IsTrue(extensions.IsElementDisplayed(headline), "Page is not loaded");
        }

        ///***********************
        ///***********************
        ///     PAGE ELEMENTS
        ///***********************
        ///***********************

        /// <summary>
        /// Select: "Department"
        /// </summary>
        public IWebElement DepartmentDropdown => driver.FindElement(By.CssSelector("p.Omada_Department>select"));

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
        /// Select: "Level"
        /// </summary>
        public IWebElement LevelDropdown => driver.FindElement(By.CssSelector("p.pi_Employement_Level>select"));

        /// <summary>
        /// Input: "Email"
        /// </summary>
        public IWebElement EmailTextBox => driver.FindElement(By.CssSelector("p.email>input"));

        /// <summary>
        /// Input: "Phone"
        /// </summary>
        public IWebElement PhoneTextBox => driver.FindElement(By.CssSelector("p.phone>input"));

        /// <summary>
        /// Select: "Country"
        /// </summary>
        public IWebElement CountryDropdown => driver.FindElement(By.CssSelector("p.country>select"));

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
        /// Fills required fields in contact form with fake data
        /// </summary>
        /// <returns>Contact page</returns>
        public ContactPage FillContactForm()
        {
            SwitchToIFrame(0);
            extensions.SelectDropdownByText(DepartmentDropdown, "Sales");
            FirstNameTextBox.SendKeys(Name.First());
            LastNameTextBox.SendKeys(Name.Last());
            CompanyTextBox.SendKeys(Company.Name());
            JobTitleTextBox.SendKeys("Test employee");
            extensions.SelectDropdownByText(LevelDropdown, "Manager");
            EmailTextBox.SendKeys(Internet.Email());
            PhoneTextBox.SendKeys(Phone.Number());
            extensions.SelectDropdownByText(CountryDropdown, "Poland");
            PrivacyPolicyRadioButton.Click();

            return this;
        }

        /// <summary>
        /// Submits empty contact form, checks if correct number of 'Field required' error messages is displayed.
        /// </summary>
        /// <param name="numberOfRequiredPopUps">Number of times, that 'Field required' error message should be displayed</param>
        /// <returns>Contact page</returns>
        public ContactPage SubmitEmptyFormAndCheckRequiredInfo(int numberOfRequiredErrors)
        {
            SwitchToIFrame(0);
            SubmitFormButton.Submit();
            By fieldRequiredPopUpSelector = By.XPath("//p[contains(text(),'This field is required')]");
            By departmentPopUpSelector = By.XPath("//span[contains(text(),'Please select the department you wish to contact')]");
            int popUpCount = driver.FindElements(fieldRequiredPopUpSelector).Count;
            IWebElement departmentPopUp = driver.FindElement(departmentPopUpSelector);
            Assert.IsTrue(extensions.IsElementDisplayed(departmentPopUp), "'Please select the department you wish to contact' pop-up is not visible");
            Assert.IsTrue(popUpCount == numberOfRequiredErrors, $"Number of 'This field is required' error messages ({popUpCount}) is different than expected ({numberOfRequiredErrors}).'");

            return this;
        }

        /// <summary>
        /// Submits filled form without captcha, checks if 'Invalid CAPTCHA' error message is displayed.
        /// </summary>
        /// <returns>Contact page</returns>
        public ContactPage SubmitFilledDemoRequestWithoutCaptchaAndCheckError()
        {
            FillContactForm();
            SubmitFormButton.Submit();
            Assert.IsTrue(extensions.IsElementDisplayed(InvalidCaptchaParagraph), "'Invalid captcha' paragraph is not displayed");

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