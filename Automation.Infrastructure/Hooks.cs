using Automation.Infrastructure.WebDriverManager.Base;
using AventStack.ExtentReports;
using BoDi;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace Automation.Infrastructure
{
    [Binding]
    public class Hooks
    {
        private const string REPORT_LOCATION = "\\Reports\\Report.html";
        private const string STEP_DEFINITION_PENDING_ERROR_MESSAGE = "Step definition pending";
        private const string REPORT_SKIP_STATUS_NO_STEP_DEFINITION = "StepDefinitionPending";
        private const string REPORT_SKIP_STATUS_UNDEFINED_STEP = "UndefinedStep";


        private readonly IObjectContainer _objectContainer;
        private static BaseDriverManager _baseDriverManager;
        private static ExtentReports _extentReports;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string reportPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))))) + REPORT_LOCATION;

            _extentReports = Reporter.CreateReport(reportPath);
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            _feature = Reporter.CreateFeatureContext(_extentReports);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _baseDriverManager = WebDriverFactory.GetWebDriverManager(ConfigurationManager.AppSettings["browser"]);
            _objectContainer.RegisterInstanceAs(_baseDriverManager);

            DataContext dataContext = new DataContext();
            _objectContainer.RegisterInstanceAs(dataContext);

            _scenario = Reporter.CreateScenarioContext(_feature);
        }

        [AfterStep]
        public void AfterStep()
        {
            Reporter.FillPassedStepsInfo(_scenario);
            Reporter.FillFailedStepsInfo(_scenario);
            Reporter.FillSkipedStepsInfo(_scenario, REPORT_SKIP_STATUS_NO_STEP_DEFINITION);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Reporter.FillSkipedStepsInfo(_scenario, REPORT_SKIP_STATUS_UNDEFINED_STEP);

            BaseDriverManager.CloseDriver();
        }

        [AfterTestRun]
        public static void CleanUp()
        {
            if (_baseDriverManager == null)
            {
                throw new NullReferenceException("BaseDriverManager isn't initialized");
            }

            _baseDriverManager.CleanUp();
            Reporter.CloseReport(_extentReports);
        }
    }
}
