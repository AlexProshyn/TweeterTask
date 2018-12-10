using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Automation.Infrastructure.WebDriverManager.Base
{
    public abstract class BaseDriverManager
    {
        protected const string DRIVERS_LOCATION = @"\Drivers";

        public static IWebDriver Driver { get; private set; }

        protected abstract IWebDriver CreateDriver();
        internal abstract void CleanUp();

        public BaseDriverManager()
        {
            if (Driver == null)
            {
                GetDriver();
            }
        }

        internal static void CloseDriver()
        {
            if (Driver == null)
            {
                throw new NullReferenceException("WebDriver isn't initialized");
            }

            Driver.Quit();
            Driver = null;
        }

        protected string GetDriversPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + DRIVERS_LOCATION;
        }

        protected void KillDriverProcessesByName(string processName)
        {
            Process[] driverProcesses = Process.GetProcessesByName(processName);

            foreach (var driverProcess in driverProcesses)
            {
                driverProcess.Kill();
            }
        }

        private void GetDriver()
        {
            if (Driver == null)
            {
                Driver = CreateDriver();
                Driver.Manage().Window.Maximize();
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
                Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
            }
        }
    }
}
