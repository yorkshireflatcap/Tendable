using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Driver
{
   

    [SetUpFixture]
    public class DriverHandler :IDisposable
    {
        private static ChromeDriver _webdriver;

        public static ChromeDriver Webdriver => _webdriver;

        [OneTimeSetUp]
        public void SetUp()
        {
            _webdriver= GetWebDriver();
            _webdriver.Manage().Window.Maximize();
            _webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            _webdriver.Navigate().GoToUrl("https://www.tendable.com/");



        }
        private ChromeDriver GetWebDriver()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArguments("start-maximised", "headless");
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
