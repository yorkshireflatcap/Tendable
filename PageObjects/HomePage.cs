using NUnit.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TendableFramework.PageObjects
{
    public class HomePage : DriverHandler
    {
        IList<IWebElement> _topLevelMenu => Webdriver.FindElements(By.CssSelector("#main-navigation-new"));
        IWebElement _contentHeader => Webdriver.FindElement(By.CssSelector(".product-hero h1"));

        IWebElement _companyLogo => Webdriver.FindElement(By.CssSelector(".logo"));
        IWebElement _bookADemo => Webdriver.FindElement(By.CssSelector(".button-links-panel a:nth-of-type(3)"));
        IWebElement _contactUsButton => Webdriver.FindElement(By.CssSelector(".button-links-panel a:nth-of-type(1)"));

        private IList<IWebElement> _currentOption;

        public HomePage()
        {
            _currentOption = new List<IWebElement>();
        }

        public void GoHome()
        {
            _companyLogo.Click();
        }
        public int getmenuItemCount
        {
            get
            {
               return _currentOption.Count();
            }
        }
        public void Hover(string name)
        {
            int count = 0;
            var menuTitle = Regex.Replace(name.ToString().ToLower(), @"\s+", "");

            var wait = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(3));

            var item = _topLevelMenu.Select(x => wait.Until(ExpectedConditions.ElementIsVisible(By.Id(menuTitle)))).First();

            //item = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementToBeClickable(item));

            Actions action = new Actions(Webdriver);
            try
            {
                action.MoveToElement(item).Perform();
            }
            catch (Exception ex)
            {
                 count = 0;
            }

            string menuOption = String.Format("#main-navigation-new a#{0}+ul li", menuTitle);

            _currentOption = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(menuOption)));
            //_currentOption = Webdriver.FindElements(By.CssSelector(String.Format("#main-navigation-new a#{0}+ul li", menuTitle))).ToList();
;
        }
        public MenuItem getMenuItemByIdx(int index)
        {
            return new MenuItem(_currentOption[index]);
        }
        public MenuItem getMenuByName(string name)
        {
            var item = _topLevelMenu.Select(x => x.FindElement(By.Id(name.ToLower())));

            if (item.Count() > 1)
            {
                throw new Exception("Duplicate menu items found");
            }
            return new MenuItem(item.First());
        }
        public ContactUs ClickContactButton()
        {
            _contactUsButton.Click();
            return new ContactUs();
        }



        public string GetContentString => _contentHeader.Text;
        public string GetCurrentUrl => Webdriver.Url;
        public bool LogoIsDisplayed => _companyLogo.Enabled && _companyLogo.Displayed;

        public bool BookADemoIsDisplayed => _bookADemo.Enabled && _bookADemo.Displayed;
    }
}
