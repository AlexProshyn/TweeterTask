using Automation.Infrastructure.WebDriverManager.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Automation.Infrastructure.WebDriverManager
{
    public class FirefoxDriverManager :  BaseDriverManager
    {
        private const string DEFAULT_FIREFOX_BINARY_PATH = @"C:\Program Files\Mozilla Firefox\firefox.exe";
        private const string GECKODRIVER_PROCESS_NAME = "geckodriver";

        public FirefoxDriverManager() : base()
        {
        }

        protected override IWebDriver CreateDriver()
        {
            string geckoDriverPath = GetDriversPath();

            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(geckoDriverPath);
            service.FirefoxBinaryPath = DEFAULT_FIREFOX_BINARY_PATH;

            return new FirefoxDriver(service);
        }

        internal override void CleanUp()
        {
            KillDriverProcessesByName(GECKODRIVER_PROCESS_NAME);
        }
    }
}
