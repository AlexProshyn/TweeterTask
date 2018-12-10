using Automation.Utilities.WebDriverHelper;
using OpenQA.Selenium;
using System;

namespace Automation.PageObjects
{
    public class StartPageObject : BasePageObject
    {
        IWebElement LoginField => driver.FindElementByName("session[username_or_email]");
        IWebElement PasswordField => driver.FindElementByName("session[password]");
        IWebElement LoginButton => driver.FindElementByClassName("js-submit");

        public StartPageObject() : base()
        {
        }

        public void OpenPage()
        {
            driver.OpenPage(url);
        }

        public bool IsOpened()
        {
            bool isLoginFieldDisplayed = IsElementDisplayed(LoginField);

            return isLoginFieldDisplayed == true;
        }

        public void Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new NullReferenceException("Login data is empty");
            }

            LoginField.SendKeys(username);
            PasswordField.SendKeys(password);
            LoginButton.Click();
            driver.WaitForAjax();
        }
    }
}