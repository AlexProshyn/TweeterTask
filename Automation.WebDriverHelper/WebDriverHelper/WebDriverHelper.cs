using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Automation.Utilities.WebDriverHelper
{
    public static class WebDriverHelper
    {
        private const int DEFAULT_TIMEOUT = 10;
        private const string SCREENSHOT_LOCATION = "\\Screenshots";

        public static void WaitForAjax(this IWebDriver driver, int timeout = DEFAULT_TIMEOUT)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
        }

        public static void WaitForElementToBeDisplayed(this IWebDriver driver, IWebElement element, int timeout = DEFAULT_TIMEOUT)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            wait.Until(d => element.Displayed);
        }

        public static void WaitForElementToBeHidden(this IWebDriver driver, IWebElement element, int timeout = DEFAULT_TIMEOUT)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            wait.Until(d => !element.Displayed);
        }

        public static void WaitForElementToBeEnabled(this IWebDriver driver, IWebElement element, int timeout = DEFAULT_TIMEOUT)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            wait.Until(d => element.Enabled);
        }

        public static void WaitForClassAppeared(this IWebDriver driver, IWebElement element, string className, int timeout = DEFAULT_TIMEOUT)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            wait.Until(d => element.GetAttribute("class").Contains(className));
        }

        public static void OpenPage(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.WaitForAjax();
        }

        public static void SendKeys(this IWebDriver driver, IWebElement element, string text)
        {
            Actions actions = new Actions(driver);
            actions.SendKeys(text).Build().Perform();
            Thread.Sleep(500);
        }

        public static bool IsElementExists(this IWebDriver driver, By by)
        {
            bool isExists = true;

            try
            {
                driver.FindElement(by);
            }
            catch(NoSuchElementException)
            {
                isExists = false;
            }

            return isExists;
        }

        public static string TakeScreenShot(this IWebDriver driver)
        {
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();

            string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))));
            string resolvedPath = path + SCREENSHOT_LOCATION + "\\" +DateTime.Now.ToLongDateString() + ".png";

            screenshot.SaveAsFile(resolvedPath, ScreenshotImageFormat.Png);

            return resolvedPath;
        }
    }
}
