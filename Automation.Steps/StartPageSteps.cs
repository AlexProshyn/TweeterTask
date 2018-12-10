using Automation.Contracts;
using Automation.PageObjects;
using Automation.Utilities.TestDataHelper;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace Automation.Steps
{
    [Binding]
    public class StartPageSteps
    {
        private const string LOGIN_DATA_PATH = "\\TestData\\LoginData.json";

        private StartPageObject _startPageObject;

        public StartPageSteps()
        {
            _startPageObject = new StartPageObject();
        }

        [Given(@"Navigate to the start page")]
        public void GivenNavigateToTheStartPage()
        {
            _startPageObject.OpenPage();
        }

        [When(@"Login from Start page using '(.*)' credentials")]
        public void WhenLoginFromStartPageUsingCredentials(string userType)
        {
            string json = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + LOGIN_DATA_PATH);

            LoginParameters loginParameters = TestDataHelper.GetLoginDataFromJson(json, userType);

            _startPageObject.Login(loginParameters.Username, loginParameters.Password);
        }

        [Then(@"Start page is opened")]
        public void GivenStartPageIsOpened()
        {
            bool isOpened = _startPageObject.IsOpened();
            Assert.IsTrue(isOpened, "Start page isn't opened");
        }
    }
}
