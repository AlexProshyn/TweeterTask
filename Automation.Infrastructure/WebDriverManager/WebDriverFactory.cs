using Automation.Infrastructure.WebDriverManager;
using Automation.Infrastructure.WebDriverManager.Base;
using System;

namespace Automation.Infrastructure
{
    public class WebDriverFactory
    {
        private const string BROWSER_CHROME = "chrome";
        private const string BROWSER_FIREFOX = "firefox";

        public static BaseDriverManager GetWebDriverManager(string browser)
        {
            BaseDriverManager driverManager = null;

            switch(browser.ToLower())
            {
                case BROWSER_CHROME:
                    driverManager = new ChromeDriverManager();
                    break;
                case BROWSER_FIREFOX:
                    driverManager = new FirefoxDriverManager();
                    break;
                default:
                    throw new InvalidOperationException("Not supported browser");
            }

            return driverManager;
        }
    }
}
