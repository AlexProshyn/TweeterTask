using Automation.Infrastructure.WebDriverManager.Base;
using Automation.Utilities.WebDriverHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Configuration;

namespace Automation.PageObjects
{
    public class BasePageObject
    {
        protected string url;
        protected RemoteWebDriver driver;

        public BasePageObject()
        {
            url = ConfigurationManager.AppSettings["twitterURL"];
            driver = (RemoteWebDriver)BaseDriverManager.Driver;
        }

        public void RefreshThePage()
        {
            driver.Navigate().Refresh();
            driver.WaitForAjax();
        }

        protected bool IsElementDisplayed(IWebElement element)
        {
            bool isDisplayed = true;

            try
            {
                driver.WaitForElementToBeDisplayed(element);
            }
            catch(WebDriverTimeoutException)
            {
                isDisplayed = false;
            }

            return isDisplayed;
        }
    }
}
