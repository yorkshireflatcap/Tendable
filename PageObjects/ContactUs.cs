using Newtonsoft.Json;
using NUnit.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendableFramework.PageObjects
{
    public class ContactDataTemplate
    {
        [JsonProperty("firstname")]
        public string ForeName { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonProperty("email")]
        public string EmailAddress { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }


        [JsonProperty("OrgName")]
        public string Organisation { get; set; }
        [JsonProperty("freeText")]
        public string FreeTextInput { get; set; }

    }
    public class ContactUs : DriverHandler
    {

        IWebElement _firstname => Webdriver.FindElement(By.Id("form-input-firstName"));
        IWebElement _lastname => Webdriver.FindElement(By.Id("form-input-lastName"));
        IWebElement _email => Webdriver.FindElement(By.Id("form-input-emailRegex"));

        IWebElement _phoneNumber => Webdriver.FindElement(By.Id("form-input-cellPhone"));
        IWebElement _orgName => Webdriver.FindElement(By.Id("form-input-organisationName"));
        IWebElement _freeText => Webdriver.FindElement(By.Id("form-input-organisationName"));

        IWebElement _agreeButton => Webdriver.FindElement(By.Id("form-input-consentAgreed-0"));
        IWebElement _submitButton => Webdriver.FindElement(By.CssSelector(".freeform-column button[name='form_page_submit']"));

        IList<IWebElement> _errorMsg => Webdriver.FindElements(By.CssSelector("#contactForm .ff-errors"));

        public ContactUs FillOutForm(ContactDataTemplate contactData)
        {
            _firstname.SendKeys(contactData.ForeName);
            _lastname.SendKeys(contactData.Surname);
            _email.SendKeys(contactData.EmailAddress);
            _orgName.SendKeys(contactData.Organisation);
            _phoneNumber.SendKeys(contactData.PhoneNumber);
            _freeText.SendKeys(contactData.FreeTextInput);


            return this;
        }

        public ContactUs AgreeButton
        {
            get
            {
                // there are times that only javascript can perform an action... and this is it!
                string javascript = "document.getElementById('form-input-consentAgreed-0').click()";
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Webdriver;

                jsExecutor.ExecuteScript(javascript);
             
                return this;
            }
        }
        public string getErrorMessage
        {
            get { return _errorMsg.First().Text; }
        }

        public int SubmitForm()
        {
            string javascript = "$(\"button[name='form_page_submit']\").click()";
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Webdriver;

            int numberOfErrors = 0;
            try
            {
                jsExecutor.ExecuteScript(javascript);
                var element = new WebDriverWait(Webdriver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".ff-errors")));
                numberOfErrors = _errorMsg.Count();
            }
            catch (Exception ex)
            {
                numberOfErrors = 0;
            }

            return numberOfErrors;
        }
    }
}
