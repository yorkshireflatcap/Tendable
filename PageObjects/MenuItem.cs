using NUnit.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendableFramework.PageObjects
{
    public class MenuItem: DriverHandler
    {

        private IWebElement _webElement;
        
        public MenuItem(IWebElement item)
        {
            _webElement = item;
        }
       
        public string MenuItemName
        {
            get { return _webElement.Text; }
        }


        public void SelectOption()
        {
            try
            {
                _webElement = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementToBeClickable(_webElement));
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("ERROR {0}", _webElement.Text));
            }
           
            _webElement.Click();

        }
    }
}
