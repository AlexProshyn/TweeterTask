using Automation.Infrastructure.WebDriverManager.Base;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System;
using System.Reflection;
using TechTalk.SpecFlow;
using Automation.Utilities.WebDriverHelper;

namespace Automation.Infrastructure
{
    internal class Reporter
    {
        private const string GIVEN_STEPS = "Given";
        private const string WHEN_STEPS = "When";
        private const string THEN_STEPS = "Then";
        private const string ERROR_MESSAGE = "ERROR";
        private const string ERROR_MESSAGE_STEP_DEFINITION_PENDING = "Step definition pending";
        private const string ERROR_MESSAGE_NO_STEP_DEFINITION = "No matching step definition found for one or more steps";
        private const string REPORT_SKIP_STATUS_UNDEFINED_STEP = "UndefinedStep";


        public static ExtentReports CreateReport(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new NullReferenceException("Report path couldn't be empty");
            }

            ExtentHtmlReporter extentHtmlReporter = new ExtentHtmlReporter(path);

            ExtentReports extentReports = new ExtentReports();
            extentReports.AttachReporter(extentHtmlReporter);

            return extentReports;
        }

        public static ExtentTest CreateFeatureContext(ExtentReports extentReports)
        {
            if (extentReports == null)
            {
                throw new NullReferenceException("Extent report isn't initialized");
            }

            return extentReports.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        public static ExtentTest CreateScenarioContext(ExtentTest featureContext)
        {
            if (featureContext == null)
            {
                throw new NullReferenceException("Feature context isn't initialized");
            }

            return featureContext.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        public static void FillPassedStepsInfo(ExtentTest scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new NullReferenceException("Scenario context isn't initialized");
            }

            if (ScenarioContext.Current.TestError == null)
            {
                string stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
                string stepText = ScenarioStepContext.Current.StepInfo.Text;

                switch (stepType)
                {
                    case GIVEN_STEPS:
                        scenarioContext.CreateNode<Given>(stepText);
                        break;
                    case WHEN_STEPS:
                        scenarioContext.CreateNode<When>(stepText);
                        break;
                    case THEN_STEPS:
                        scenarioContext.CreateNode<Then>(stepText);
                        break;
                }
            }
        }

        public static void FillFailedStepsInfo(ExtentTest scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new NullReferenceException("Scenario context isn't initialized");
            }

            if (ScenarioContext.Current.TestError != null)
            {
                string stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
                string stepText = ScenarioStepContext.Current.StepInfo.Text;
                string errorMessage = ScenarioContext.Current.TestError.Message;

                switch (stepType)
                {
                    case GIVEN_STEPS:
                        scenarioContext.CreateNode<Given>(stepText).Fail(errorMessage);
                        break;
                    case WHEN_STEPS:
                        scenarioContext.CreateNode<When>(stepText).Fail(errorMessage);
                        break;
                    case THEN_STEPS:
                        scenarioContext.CreateNode<Then>(stepText).Fail(errorMessage);
                        break;
                }

                scenarioContext.AddScreenCaptureFromPath(BaseDriverManager.Driver.TakeScreenShot());
            }
        }

        public static void FillSkipedStepsInfo(ExtentTest scenarioContext, string skipTestResult)
        {
            if (scenarioContext == null)
            {
                throw new NullReferenceException("Scenario context isn't initialized");
            }

            PropertyInfo propertyInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus");
            MethodInfo methodInfo = propertyInfo.GetGetMethod(nonPublic: true);
            object testResult = methodInfo.Invoke(ScenarioContext.Current, null);

            if (testResult.ToString().Equals(skipTestResult))
            {
                if (!skipTestResult.Equals(REPORT_SKIP_STATUS_UNDEFINED_STEP))
                {
                    string stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
                    string stepText = ScenarioStepContext.Current.StepInfo.Text;

                    switch (stepType)
                    {
                        case GIVEN_STEPS:
                            scenarioContext.CreateNode<Given>(stepText).Skip(ERROR_MESSAGE_STEP_DEFINITION_PENDING);
                            break;
                        case WHEN_STEPS:
                            scenarioContext.CreateNode<When>(stepText).Skip(ERROR_MESSAGE_STEP_DEFINITION_PENDING);
                            break;
                        case THEN_STEPS:
                            scenarioContext.CreateNode<Then>(stepText).Skip(ERROR_MESSAGE_STEP_DEFINITION_PENDING);
                            break;
                    }
                }
                else
                {
                    scenarioContext.CreateNode<Then>(ERROR_MESSAGE).Skip(ERROR_MESSAGE_NO_STEP_DEFINITION);
                }
            }
        }

        public static void CloseReport(ExtentReports extentReports)
        {
            if (extentReports == null)
            {
                throw new NullReferenceException("Extent report isn't initialized");
            }

            extentReports.Flush();
        }
    }
}
