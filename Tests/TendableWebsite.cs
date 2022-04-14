using NUnit.Framework;
using NUnit.Driver;
using TendableFramework.PageObjects;
using System.IO;

namespace NUnit.Tests
{
    [TestFixture]
    class TendableWebsite: DriverHandler
    {
        private HomePage _homePage = new HomePage();


        static object[] topLevelMenus = {
            new object[] { "Features", "Why Tendable", "Resources", "Company" }
        };
 
        [TestCase("Home", 
         Description ="The page displayed should be the correct page", 
         ExpectedResult = "https://www.tendable.com/")
        ]
        [TestCase("Content", 
         Description = "Is the main content displayed", 
         ExpectedResult = "Unleash the potential of your teams to improve quality in health and social care")
        ]
        public string DoesTheHomePageDisplayCorrectly(string testType)
        { 
            string actual = string.Empty;

            if (testType == "Home") 
            {
                _homePage.GoHome();
                actual =  _homePage.GetCurrentUrl;
            }
            else
            {
                actual = _homePage.GetContentString;
            }
            return actual;
        }
        [Test(
         Description = "Check if the named menu items exist as well as the company logo"), 
         TestCaseSource(nameof(topLevelMenus))]
        public void HoverOverAllTopLevelMenuItemsToDisplayDropdown(object[] menuItemTitles)
        {
            _homePage = new HomePage();
            foreach (var itemMenu in menuItemTitles)
            {
                _homePage.Hover(itemMenu);
            }
            // lastly, check for the presence of the logo.
            Assert.IsTrue(_homePage.LogoIsDisplayed, "Cannot find Logo");
        }
        [Test(
         Description = "Book a Demo button is available to all items under the main menu title"),
         TestCaseSource(nameof(topLevelMenus))]
        public void EveryOptionClickedInDropdownShouldBookADemo(object[] menuItemTitles)
        {
            _homePage = new HomePage();

            foreach (var itemMenu in menuItemTitles)
            {
                _homePage.Hover(itemMenu);
                for (int i = 0; i < _homePage.getmenuItemCount; i++)
                {
                    // As the DOM is being refreshed after every sub menu item selection, we need to keep track of the current position
                    _homePage.getMenuItemByIdx(i).SelectOption();
                    Assert.IsTrue(_homePage.BookADemoIsDisplayed);
                    _homePage.Hover(itemMenu);
                }
            }
        }
        [Test(
         Description = "Omitting the message field will result in 1 error with a message 'This field is Required' being displayed", ExpectedResult="This field is required"),
            ]
        public string PartiallyFillingInContactUsFormShouldResultInError()
        {
            StreamReader r = new StreamReader("../../../TestData/contactData.json");
            string jsonString = r.ReadToEnd();
            var dataStruct = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactDataTemplate>(jsonString);
            var contactUs = new HomePage().ClickContactButton();
            int numberOfErrors = contactUs.FillOutForm(dataStruct).AgreeButton.SubmitForm();

            Assert.AreEqual(numberOfErrors, 1);
            string mesg = contactUs.getErrorMessage;
            return mesg;
        }   
        
    }
}
