using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OmadaTestAssingment.SeleniumHelpers
{
    internal class WebExtensions
    {
        /// <summary>
        /// Checks if provided element is displayed on the page
        /// </summary>
        /// <param name="element">Element that needs to be checked</param>
        /// <returns>Bool - Is element displayed</returns>
        public bool IsElementDisplayed(IWebElement element)
        {
            return element.Displayed;
        }

        /// <summary>
        /// Selects one of the item from the dropdown using text value
        /// </summary>
        /// <param name="element">Dropdown element</param>
        /// <param name="textToSelect">Text by which the value is selected</param>
        public void SelectDropdownByText(IWebElement element, string textToSelect)
        {
            new SelectElement(element).SelectByText(textToSelect);
        }
    }
}