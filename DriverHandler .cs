using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendableFramework.PageObjects;
using Microsoft.Extensions.DependencyInjection;

namespace NUnit.Driver
{
    public interface IHomePage 
    {
        void GoHome();
        void Hover(string name);
        MenuItem getMenuItemByIdx(int index);
        MenuItem getMenuByName(string name);
        ContactUs ClickContactButton();
    }

    [SetUpFixture]
   
    public class DriverHandler :IDisposable
    {
        private static ChromeDriver _webdriver;

        public static ChromeDriver Webdriver => _webdriver;

        [OneTimeSetUp]
        public void SetUp()
        {
            _webdriver= GetWebDriver();
            _webdriver.Manage().Window.Maximize(); // remove this as it is max headless already
            _webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            _webdriver.Navigate().GoToUrl("https://www.tendable.com/");

        }
        private ChromeDriver GetWebDriver()
        {
            ChromeOptions option = new ChromeOptions();
            //option.AddArguments("start-maximised", "headless");
            new DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver(option);
        }

        public void Dispose()
        {
            Webdriver.Close();
            Webdriver.Quit();
        }
    }
}
