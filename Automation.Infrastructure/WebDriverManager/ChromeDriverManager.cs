using Automation.Infrastructure.WebDriverManager.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation.Infrastructure.WebDriverManager
{
    public class ChromeDriverManager : BaseDriverManager
    {
        private const string CHROME_DRIVER_PROCESS_NAME = "chromedriver";

        public ChromeDriverManager() : base()
        {
        }

        protected override IWebDriver CreateDriver()
        {
            string chromeDriverPath = GetDriversPath();

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--disable-extensions");

            return new ChromeDriver(chromeDriverPath, chromeOptions);
        }

        internal override void CleanUp()
        {
            KillDriverProcessesByName(CHROME_DRIVER_PROCESS_NAME);
        }
    }
}
